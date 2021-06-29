using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Feature.Body.Models;

namespace BlogProject.Feature.Body.Controllers
{
    public class BodyController : Controller
    {


        public ActionResult BlogDetail(string ID)
        {



            BodyModel model = new BodyModel();

            var blog_item = Sitecore.Context.Database.GetItem(ID);

            model.ShortDescription = blog_item["ShortDescription"];
            model.BlogTitle = blog_item["BlogTitle"];
            model.BlogCategory = blog_item["BlogCategory"];
            model.LongDescription = blog_item["LongDescription"];

            model.BlogPostDate = Sitecore.DateUtil.IsoDateToDateTime(blog_item["BlogPostDate"]);



            Sitecore.Data.Fields.ImageField imageField = blog_item.Fields["SmallImage"];
            Sitecore.Data.Fields.ImageField LargeimageField = blog_item.Fields["LargeImage"];

            if (imageField != null && imageField.MediaItem != null || (LargeimageField != null && LargeimageField.MediaItem != null))
            {
                Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
                Sitecore.Data.Items.MediaItem Limage = new Sitecore.Data.Items.MediaItem(LargeimageField.MediaItem);

                string src = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));

                string Lsrc = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(Limage));

                string imgTag = String.Format(@"<img src=""{0}"" alt=""{1}"" />", src, image.Alt);

                string LimgTag = String.Format(@"<img src=""{0}"" alt=""{1}"" />", Lsrc, Limage.Alt);

                model.SmallImage = imgTag;
                model.LargeImage = LimgTag;


            }



            return View("~/Views/Body/BlogDetail.cshtml", model);

        }

        public ActionResult Body()
        {

            BodyModel model = new BodyModel();

            var results = new List<BodyModel>();

            Sitecore.Data.Database database = Sitecore.Configuration.Factory.GetDatabase("web");
            Item[] contextItem = database.SelectItems("/sitecore/content/home/BlogProject/Home/*[@@templateid='{4411198A-4669-4EE3-ABFF-2FA59C18A189}']");


            foreach (var hit in contextItem)
            {
                Sitecore.Data.Fields.ImageField imageField = hit.Fields["SmallImage"];
                Sitecore.Data.Fields.ImageField LargeimageField = hit.Fields["LargeImage"];

                if (imageField != null && imageField.MediaItem != null || (LargeimageField != null && LargeimageField.MediaItem != null))
                {
                    Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
                    Sitecore.Data.Items.MediaItem Limage = new Sitecore.Data.Items.MediaItem(LargeimageField.MediaItem);

                    string src = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));

                    string Lsrc = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(Limage));

                    string imgTag = String.Format(@"<img src=""{0}"" alt=""{1}"" />", src, image.Alt);

                    string LimgTag = String.Format(@"<img src=""{0}"" alt=""{1}"" />", Lsrc, Limage.Alt);

                    results.Add(new BodyModel
                    {
                        ShortDescription = hit["ShortDescription"],
                        BlogTitle = hit["BlogTitle"],
                        BlogCategory = hit["BlogCategory"],
                        SmallImage = imgTag,
                        LargeImage = LimgTag,                        
                        LongDescription = hit["LongDescription"],                       
                        BlogPostDate = Sitecore.DateUtil.IsoDateToDateTime(hit["BlogPostDate"]),
                        ID = hit["Id"]
                       
                    });
                    

                }
            }
            return View("~/Views/Body/Body.cshtml",model);
           
        }
    }
}