using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.DomainModels
{
    public class Exam
    {
       
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ChangeDate { get; set; }
        public DateTime? SendReminderDate { get; set; }
        public TimeSpan? ExamTime { get; set; }
        public bool RandomOrder { get; set; }
        public ICollection<Question> Questions { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    
    }
}