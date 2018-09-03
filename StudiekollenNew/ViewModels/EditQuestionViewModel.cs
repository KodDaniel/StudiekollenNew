﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.ViewModels
{
    public class EditQuestionViewModel
    {
        // Logik (Data Annotations) finns här eftersom detta ej har direkt med databas att göra

        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Display(Name = "Fråga")]
        public string Query { get; set; }

        [Display(Name = "Svar")]
        public string Answer { get; set; }

        public int TestId { get; set; }
    }
}