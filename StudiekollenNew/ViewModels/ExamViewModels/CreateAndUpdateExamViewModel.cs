using System;
using System.ComponentModel.DataAnnotations;

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
        public TimeSpan? ExamTime { get; set; }
        [Display(Name = "Aktivera mejlpåminnelse")]
        public bool ReminderDateBool { get; set; }
        [Display(Name = "Skicka mejlpåminnelse")]
        [DataType(DataType.Date)]
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
