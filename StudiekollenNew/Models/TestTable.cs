using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class TestTable
    {
        #region Tabellattribut
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required()]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Provnamn")]
        public string Name { get; set; }

        public System.DateTime CreateDate { get; set; } /*= DateTime.UtcNow;*/

        public System.DateTime? ChangeDate { get; set; } /*= DateTime.UtcNow;*/
        #endregion

        #region Tabellrelationer
        public ICollection<QuestionTable> QuestionTable { get; set; }
        public virtual User User { get; set; }
        #endregion


    }
}