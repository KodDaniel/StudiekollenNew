using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.CustomValidaiton
{
    public class DateCustomValidation:ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            else
            {
                var datetime = Convert.ToDateTime(value);
                return   datetime > DateTime.Now;

             
            }

        }


    }
}