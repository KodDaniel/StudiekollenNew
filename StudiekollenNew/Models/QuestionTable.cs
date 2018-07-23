using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{

    public class QuestionTable
    {   
        public int Id{ get; set; }

        [Display(Name = "Fråga")]
        public string Question{ get; set; }

        [Display(Name = "Svar")]
        public string Answer { get; set; }

        public string Result { get; set; }

        //Främmande nyckel.
        public int TestId { get; set; }

        public virtual TestTable TestTable { get; set; }



    }
}