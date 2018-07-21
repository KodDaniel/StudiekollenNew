using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    //[Table(name:"TestTable")]
    public class Test
    {
        //Blir automatiskt autoinkrementerd primary key
        public int TestId { get; set; }

        //Främmande nyckel.
        public int UserId{ get; set; }

        [Required()]
        [StringLength(100,MinimumLength = 2)]
        public string Name { get; set; }

        public System.DateTime CreateDate { get; set; }

        // Frågetecknet gör så att vi tillåter nullvärden? Ett prov kan ju aldrig ha ändrats.
        public System.DateTime? ChangeDate { get; set; }

    }
}