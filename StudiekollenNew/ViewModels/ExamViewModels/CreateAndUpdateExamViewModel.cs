using System;
using System.Collections;
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
        //Anropar egen customvalideringsklass
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

        //public ValidationResult CustomTimeValidation(bool examTimeBool,TimeSpan? examTime)
        // {
        //     var listOfConcerdedProperties = new List<string> {"ExamTime"};

        //     // Om ExamTime är null behöver vi inte validera
        //     if (examTime == null)
        //     {
        //         return  ValidationResult.Success;
        //     }
        //     if (examTimeBool && examTime  == null)
        //     {
        //         return new ValidationResult("Du måste fylla i en provtid", listOfConcerdedProperties);

        //     }
        //     //Kolla så att den angivna tiden är längre än en minut
        //     if (examTime < new TimeSpan(0, 0, 1, 0))
        //     {
        //         return new ValidationResult("Du måste fylla i en provtid som är längre än en minut",
        //             listOfConcerdedProperties);

        //     }

        //     return   ValidationResult.Success;

        // }


        // }
    }
}
