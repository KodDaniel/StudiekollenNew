using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels
{
    public class FindTestViewModel
    {
        [Required(ErrorMessage = "Du måste välja en användare.")]
        public string Username { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Test> AllTests { get; set; }
        
       
    
    }

}