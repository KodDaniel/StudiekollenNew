using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels
{
    public class CreateTestViewModel
    {
       

        public string Name { get; set; }

        [Display(Name = "Fråga")]
        public string Query { get; set; }

        [Display(Name = "Svar")]
        public string Answer { get; set; }


        public void  AddToDictionary (string question, string answer)
        {
           //dictionary.Add(question,answer);
        }

     
    }
}