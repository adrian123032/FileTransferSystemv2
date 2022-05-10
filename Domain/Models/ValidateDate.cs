using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ValidateDate : ValidationAttribute
    {
        private readonly DateTime def = new DateTime(0001, 01, 01, 00, 00, 00);

        public override bool IsValid(object value)
            
        {
            string valueAsString = value.ToString();
            DateTime dt = DateTime.Parse(valueAsString);
               
            if ((dt < DateTime.Today) && (dt != def))
            {
                return false;
            }
            return true;
        }

    }
}
