using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Feature.Body.Models
{
    public class BodyModel
    {
        
        public virtual string ID { get; set; }
        public virtual string BlogTitle { get; set; }
        public virtual string BlogCategory { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string LongDescription { get; set; }

        public virtual string SmallImage { get; set; }


        public virtual string LargeImage { get; set; }
        public virtual DateTime BlogPostDate { get; set; }
        public virtual List<string> Tags { get; set; }

       

    }
    
}