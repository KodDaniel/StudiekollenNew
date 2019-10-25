using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Repositories;

namespace StudiekollenNew.Services
{
    public class MetaTagService
    {
        private MetaTagRepository _metaTagRepository;

        public MetaTagService(RepositoryFactory repoFactory)
        {
            _metaTagRepository = repoFactory.GetMetaTagRepository();
        }

        public MetaTags GetPageMetaTags(string pageUrl)
        {
            return _metaTagRepository.GetPageMetaTags(pageUrl);
        }

        public void UpdateMetaTags(MetaTags ms, string pageUrl)
        {
         _metaTagRepository.GetPageMetaTags(pageUrl);
        }

    }
}