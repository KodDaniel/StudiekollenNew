using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class TestTable
    {
        [Key]
        public int TestId { get; set; }

        public string UserId { get; set; }

        [Required()]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }

        public System.DateTime CreateDate { get; set; }

        public System.DateTime? ChangeDate { get; set; }

        public virtual UserTable User { get; set; }

    }
}