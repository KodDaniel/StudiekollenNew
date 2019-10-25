using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using StudiekollenNew.Models;

namespace StudiekollenNew.DomainModels
{
    public class MetaTags
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PageUrl { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }

    }
}