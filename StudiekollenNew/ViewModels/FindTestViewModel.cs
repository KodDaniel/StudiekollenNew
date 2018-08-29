using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels
{
    public class FindTestViewModel
    {
        public IEnumerable<string> AllTests { get; set; }
        //public IEnumerable<Test> Tests { get; set; }
        public string Username { get; set; }
    }
}