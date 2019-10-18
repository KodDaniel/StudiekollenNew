using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.ViewModels.ExamViewModels

{
    public class CreateExamViewModel
    {
        [Display(Name = "Provnamn")]
        [Required(ErrorMessage = "Du måste fylla i ett provnamn")]
        [StringLength(75, ErrorMessage = "Du kan inte ett provnamn som är längre än 100 tecken.")]
        public string ExamName { get; set; }
        [Display(Name = "Aktivera tidtagning")]
        public bool ExamTimeBool { get; set; }
        [Display(Name = "Önskat tidsintervall")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public TimeSpan? ExamTime { get; set; }
        [Display(Name = "Aktivera mejlpåminnelse")]
        public bool ReminderDateBool { get; set; }
        [Display(Name = "Skicka mejlpåminnelse")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime? SendReminderDate { get; set; }
        [Display(Name = "Slumpmässig ordning")]
        public bool RandomOrder { get; set; }
        [Display(Name = "Önskat tidsintervall")]
        [DataType(DataType.Time)]
        public string DataFormatString { get; set; }


    }
}