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
        [Key]
        public int QuestionId { get; set; }

        public string Question{ get; set; }

        public string Answer { get; set; }



        ////Främmande nyckel.
        //public int TestId { get; set; }

        //// Kan ej ha Question som attibut då klassen heter det. Därför Query.
        //[Required()]
        //public string Query { get; set; }

        //[Required()]
        //public string Answer { get; set; }


    }
}