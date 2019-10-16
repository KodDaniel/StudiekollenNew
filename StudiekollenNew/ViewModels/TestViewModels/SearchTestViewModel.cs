using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels
{
    public class SearchTestViewModel
    {
        public IEnumerable<Test> AllTests { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett provnamn")]

        public int Id {get; set; }




    }

}