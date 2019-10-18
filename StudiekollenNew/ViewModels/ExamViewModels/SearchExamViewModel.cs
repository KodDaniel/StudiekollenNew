using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels.ExamViewModels
{
    public class SearchExamViewModel
    {
        public IEnumerable<Exam> AllExams { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett provnamn")]
        public int Id { get; set; }
    }

}