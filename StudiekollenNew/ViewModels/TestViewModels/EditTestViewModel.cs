using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels.TestViewModels
{
    public class EditTestViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public IEnumerable<Question> QuestionsModels { get; set; }

 
    

      
    }
}