using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using StudiekollenNew.ViewModels.CustomValidaiton;
using StudiekollenNew.ViewModels.CustomValidation;

namespace StudiekollenNew.ViewModels.ExamViewModels

{
    public class CreateAndUpdateExamViewModel
    {
        [Display(Name = "Provnamn")]
        [Required(ErrorMessage = "Du måste fylla i ett provnamn")]
        [StringLength(75, ErrorMessage = "Du kan inte ett provnamn som är längre än 100 tecken")]
        public string ExamName { get; set; }
        [Display(Name = "Aktivera tidtagning")]
        public bool ExamTimeBool { get; set; }
        [Display(Name = "Önskat tidsintervall (H:MM)")]
        [DataType(DataType.Time)]
        // Anropar egen customvalideringsklass
        [TimeCustomValidaiton(ErrorMessage = "Ett prov måste pågå under minst en minut")]
        public TimeSpan? ExamTime { get; set; }
        [Display(Name = "Aktivera mejlpåminnelse")]
        public bool ReminderDateBool { get; set; }
        [Display(Name = "Skicka mejlpåminnelse")]
        [DataType(DataType.Date)]
        // Anropar egen customvalideringsklass
        [DateCustomValidation(ErrorMessage = ("Du måste fylla i ett datum som inte redan varit"))]
        public DateTime? SendReminderDate { get; set; }
        [Display(Name = "Slumpmässig ordning")]
        public bool RandomOrder { get; set; }

    


    }
}