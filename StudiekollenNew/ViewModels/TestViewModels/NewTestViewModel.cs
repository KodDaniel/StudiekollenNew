using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.TestViewModels
{
    public class NewTestViewModel
    {
        public int TestId { get; set; }

        [Required]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }

       
    }
}