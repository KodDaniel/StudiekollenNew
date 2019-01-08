using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class Test
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett provnamn.")]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? ChangeDate { get; set; }
        public ICollection<Question> Questions { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}