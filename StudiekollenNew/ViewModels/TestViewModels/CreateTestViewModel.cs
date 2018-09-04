using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels
{
    public class CreateTestViewModel
    {
        // Logik (Data Annotations) finns här eftersom detta ej har direkt med databas att göra
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