using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Models;

namespace StudiekollenNew.Repositories
{
    public class MetaTagRepository
    {
        private readonly ApplicationDbContext _context;

        public MetaTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public MetaTags GetPageMetaTags(string pageUrl)
        {
           // Ad-hoc lösning i brist på LINQ-alternativ till SQLs LIKE-operator
            return _context.MetaTags.SingleOrDefault(a => a.PageUrl == pageUrl);
        }

        public void UpdateMetaTags(MetaTags ms, string pageUrl)
        {
            var currentMetaTags= GetPageMetaTags(pageUrl);

            currentMetaTags.PageUrl = ms.PageUrl;
            currentMetaTags.MetaDescription = ms.MetaDescription;
            currentMetaTags.MetaKeyWords = ms.MetaKeyWords;
            currentMetaTags.Title = ms.Title;

            _context.SaveChanges();
        }
    }

}
