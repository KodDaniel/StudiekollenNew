//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace StudiekollenNew.Models
//{
//    //[Table(name:"ResultTable")]

//    public class Result
//    {
//        //Blir automatiskt autoinkrementerd primary key
//        public int ResultId { get; set; }

//        //Främmande nyckel. String istället för int då ASP.identity använder string-format för UserId
//        public string UserId { get; set; }
       
//        //Främmande nyckel.
//        public int QuestionId { get; set; }
       
//        //Främmande nyckel.
//        public int TestId { get; set; }

//        [Required()]
//        [StringLength(50, MinimumLength = 2)]
//        // Kan ej ha Result som attibut då klassen heter det. Därför outcome.
//        public string Outcome { get; set; }



//    }
//}