using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    //STEP8
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Rollnamn")] public string Name { get; set; }
    }
}