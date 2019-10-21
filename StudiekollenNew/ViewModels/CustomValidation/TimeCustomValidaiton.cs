using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.CustomValidation
{
    public class TimeCustomValidaiton:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                var time = (TimeSpan)value;

                return time > new TimeSpan(0, 0, 1, 0);

            }
        }
    }
}