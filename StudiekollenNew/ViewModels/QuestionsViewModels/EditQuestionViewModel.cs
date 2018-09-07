using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels
{
    public class EditQuestionViewModel
    {
        public int TestId { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "Fråga")]
        public string Query { get; set; }

        [Display(Name = "Svar")]
        public string Answer { get; set; }

        public int QuestionId { get; set; }


    }
}