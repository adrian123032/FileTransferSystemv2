using System;
using System.Collections.Generic;
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
            else if (!ModelState.IsValid)
            {
                ViewBag.Error = "If required, File Expiry should not be in a past or present date!";
                return View();
            }
            else
            {
                //start uploading the file
                if (file != null)
                {



                    if (file.Length > 0)
                    {
                        //Getting file Extension
                        var fileExtension = Path.GetExtension(file.FileName);
                        // concatenating  FileName + FileExtension
                        var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);



                        model.Name = newFileName;
                        model.FileType = fileExtension;
                        model.fileLength = file.Length;

                        using (var target = new MemoryStream())
                        {
                            file.CopyTo(target);
                            model.DataFiles = target.ToArray();

                            target.Close();


                            //    using (ZipFile zip = new ZipFile())
                            //    {
                            //        if (model.Password != null)
                            //        {
                            //            zip.Password = model.Password;
                            //            zip.AddFile(absolutePath, @"\");
                            //            zip.Save(@"wwwroot/files/" + fileName + ".zip");
                            //        }
                            //        else
                            //        {
                            //            zip.AddFile(absolutePath, @"\");
                            //            zip.Save(@"wwwroot/files/" + fileName + ".zip");
                            //        }
                            //    }


                            //model.FilePath = @"\files\" + fileName + ".zip";
                            //    }
                        }
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

                string[] emails = model.ToEmail.Split(new string[] { ", ", "," }, StringSplitOptions.None);
                List<EmailAddress> list = new List<EmailAddress>();
                foreach (string email in emails)
                {
                    list.Add(new EmailAddress(email, "Recipient"));
                }
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(model.FromEmail, "Sender");
                var subject = model.Title;
                var to = list;
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
                //https://localhost:44361 local
                //http://namri99-001-site1.etempurl.com deploy
                string fullPath = "https://localhost:44361" + model.Name;
                var htmlContent = "Message:<br/>" + message + "<br/><br/>Link to your file(s): <br/><a download>" + fullPath + "</a><br/><br/><b> Password: <br/>" + password + " </b>";
                var test = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(test);
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

            }

        }
    }
}
