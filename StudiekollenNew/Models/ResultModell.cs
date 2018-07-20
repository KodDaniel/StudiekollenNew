using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class ResultModell
    {
        public int ResultId { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public int TestId { get; set; }

        public string Result { get; set; }

    }
}