using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels.TestViewModels
{
    public class EditTestViewModel
    {
        public string TestName { get; set; }
        public Dictionary<string,string> QuestionsModels { get; set; }

        public static Dictionary<string, string> ToDictionary(List<Question> questionModelsList)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var item in questionModelsList)
            {
                dictionary.Add(item.Query,item.Answer);
            }

            return dictionary;
        }
    

      
    }
}