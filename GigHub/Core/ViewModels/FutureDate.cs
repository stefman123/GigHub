using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Core.ViewModels
{
    //Using the ValidationAttribute allows you to use the class as a custom validation attribute
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
           var isValid = DateTime.TryParseExact(Convert.ToString(value), 
                "d MMM yyyy", 
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, 
                out dateTime);

            return (isValid && dateTime > DateTime.Now);
        }
    }
}