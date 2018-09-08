using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? ChangeDate { get; set; }
        public string UserId { get; set; }
    }
}