using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ValidateDate : ValidationAttribute
    {
        private readonly DateTime def = new DateTime(0001, 01, 01, 00, 00, 00);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            
        {
            
               
            if (((DateTime)value < DateTime.Today) && ((DateTime)value != def))
            {
                var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }
            return ValidationResult.Success;
        }

    }
}
