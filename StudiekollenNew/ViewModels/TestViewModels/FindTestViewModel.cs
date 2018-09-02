using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels
{
    public class FindTestViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string Username{ get; set; }
        public IEnumerable<Test> AllTests { get; set; }
        //public int QuestionId { get; set; }

    }

}