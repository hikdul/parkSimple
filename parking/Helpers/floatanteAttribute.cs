using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace parking.Helpers
{
    public class floatanteAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //\d{1,3}[,\\.]?(\\d{1,2})?

            if (String.IsNullOrEmpty(value.ToString()) || value == null)
                return ValidationResult.Success;

            var match = Regex.IsMatch(value.ToString(), @"\d{1,3}[,\\.]?(\\d{1,2})?");

            if (match)
                return ValidationResult.Success;

            return new ValidationResult("El Valor No Es Valido");
            //return base.IsValid(value, validationContext);
        }
    }
}
