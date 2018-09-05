using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels
{
    public class CreateTestViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Fråga")]
        public string Query { get; set; }

        [Required]
        [Display(Name = "Svar")]
        public string Answer { get; set; }

     
    }
}