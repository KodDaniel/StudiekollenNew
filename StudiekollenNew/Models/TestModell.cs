using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class TestModell
    {
        public int TestId { get; set; }

        public int UserId{ get; set; }

        public System.DateTime CreateDate { get; set; }

        public System.DateTime ChangeDate { get; set; }

    }
}