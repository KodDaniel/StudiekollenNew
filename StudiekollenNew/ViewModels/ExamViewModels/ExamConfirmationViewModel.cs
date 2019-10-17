using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.ExamViewModels
{
    public class ExamConfirmationViewModel
    {

        [Display(Name = "Provnamn")]
        public string ExamName { get; set; }
        [Display(Name = "Tidtagning")]
        public string Timekeeping { get; set; }
        [Display(Name = "Provlängd")]
        public string Duration { get; set; }
        [Display(Name = "Mejlpåminnelse")]
        public string SendReminder { get; set; }
        [Display(Name = "Mejl skickas")]
        public string ReminderDate { get; set; }
        [Display(Name = "Slumpmässig ordning")]
        public string Randomorder { get; set; }


    }
}