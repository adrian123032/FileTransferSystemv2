using System;
using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Ionic.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Presentation.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class EmailsController : Controller
    {
        private IEmailsService emailsService;
        private IWebHostEnvironment webHostEnvironment;
        public EmailsController(IEmailsService _emailsService, IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
            emailsService = _emailsService;
        }


        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       
        public IActionResult List()
        {
            var list = emailsService.GetFiles(@User.Identity.Name);
            return View(list);
        }

        
        [HttpPost]
        public IActionResult Create(AddEmailViewModel model, IFormFile file)
        {
            if (string.IsNullOrEmpty(model.ToEmail))
            {
                ViewBag.Error = "Recipient's Email should not be left empty!";
                return View();
            }
            else if (string.IsNullOrEmpty(model.FromEmail))
            {
                ViewBag.Error = "Sender's Email should not be left empty!";
                return View();
            }
            else if (string.IsNullOrEmpty(model.Title))
            {
                ViewBag.Error = "Title should not be left empty!";
                return View();
            }
            else
            {
                //start uploading the file
                if (file != null)
                {
                    //1. to give the file a unique name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    //2. to read the absolute path where we are going to save the file
                    string absolutePath =  webHostEnvironment.WebRootPath+ "\\files\\"+ fileName;

                    //3. we save the physical file on the web server
                    using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write ))
                    {
                        file.CopyTo(fs);

                        using(ZipFile zip = new ZipFile())
                        {
                            if(model.Password != null)
                            {
                                zip.Password = model.Password;
                                zip.AddFile(absolutePath, @"\");
                                zip.Save(@"wwwroot/files/" + fileName + ".zip");
                            }
                            else
                            {
                                zip.AddFile(absolutePath, @"\");
                                zip.Save(@"wwwroot/files/" + fileName + ".zip");
                            }
                        }
                        fs.Close(); 
                    }
                    model.FilePath = @"\files\" + fileName + ".zip";
                }
                else
                {
                    ViewBag.Error = "A File must be uploaded!";
                    return View();
                }
                
            }

            Execute(model).Wait();
            emailsService.AddEmail(model);
            ViewBag.Message = "File transferred successfully!";
            return View();
        }

        static async Task Execute(AddEmailViewModel model)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(model.FromEmail, "Sender");
            var subject = model.Title;
            var to = new EmailAddress(model.ToEmail, "Recipient");
            var plainTextContent = "";
            var password = "";
            var message = "";
            if (model.Message != null)
            {
                message = model.Message;
            }
            else
            {
                message = "N/A";
            }
            if (model.Password != null)
            {
                password = model.Password;
            }
            else
            {
                password = "N/A";
            }
            //https://localhost:44361 local
            //http://namri99-001-site1.etempurl.com deploy
            string fullPath = "https://localhost:44361" + model.FilePath;
            var htmlContent = "Message:<br/>"+ message + "<br/><br/>Link to your file(s): <br/><a download>" + fullPath + "</a><br/><br/><b> Password: <br/>" + password + " </b>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

    }
}
