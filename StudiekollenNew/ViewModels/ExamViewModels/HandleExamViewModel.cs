using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels.ExamViewModels
{
    public class HandleExamViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public IEnumerable<Question> QuestionsModels { get; set; }

 
    

      
    }
}