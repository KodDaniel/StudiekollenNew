using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels
{
    // Logik (Data Annotations) finns här eftersom detta ej har direkt med databas att göra

    public class NewTestViewModel
    {
        [Required]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }
    }
}