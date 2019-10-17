using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.TestViewModels
{
    // Denna klass kommer fyllas på med egenskaper när applikationen blir större.
    public class UpdateExamViewModel
    {
        public int ExamId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett provnamn.")]
        [Display(Name = "Provnamn")]
        public string ExamName { get; set; }
    }
}