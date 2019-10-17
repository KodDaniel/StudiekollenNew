using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels
{
    public class CreateExamViewModel
    {
        public int ExamId { get; set; }

        public string ExamName { get; set; }

        [Required (ErrorMessage = "Du måste fylla i en fråga.")]
        [Display(Name = "Fråga")]
        public string Query { get; set; }

        [Display(Name = "Svar")]
        public string Answer { get; set; }

     
    }
}