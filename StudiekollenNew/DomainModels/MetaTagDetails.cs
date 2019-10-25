using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudiekollenNew.DomainModels
{
    public class MetaTagDetails
    {
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
    }
}