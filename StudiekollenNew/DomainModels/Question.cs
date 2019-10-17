using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using StudiekollenNew.DomainModels;

namespace StudiekollenNew.Models
{

    public class Question
    {

        public int QuestionId { get; set; }
        public string Query { get; set; }
        public string Answer { get; set; }
        public Exam Exam { get; set; }
        public int ExamId { get; set; }


        //public int Id { get; set; }
        //[Required(ErrorMessage = "Du måste fylla i en fråga.")]
        //public string Query { get; set; }
        //public string Answer { get; set; }
        //public string Result { get; set; }
        //public Test Test { get; set; }
        //public int ExamId { get; set; }



    }
}