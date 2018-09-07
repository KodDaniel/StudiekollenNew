using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.TestViewModels
{
    public class NewTestViewModel
    {
        [Required]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }

       
    }
}