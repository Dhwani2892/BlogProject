using BlogProject.Feature.Postcard.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogProject.Feature.Postcard.Controllers
{
    public class PostcardController : Controller
    {
        // GET: Postcard
        public ActionResult Postcard()
        {

            Result model = new Result();
            var result = new List<Article>();
            try
            {
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

                        result.Add(new Article
                        {
                            ShortDescription = hit["ShortDescription"],
                            BlogTitle = hit["BlogTitle"],
                            BlogCategory = hit["BlogCategory"],
                            SmallImage = imgTag,
                            LargeImage = LimgTag,
                            // SmallImage = hit["SmallImage"],
                            // LargeImage = hit["LargeImage"],
                            LongDescription = hit["LongDescription"],
                            //model.BlogPostDate = Convert.ToDateTime(hit["BlogPostDate"]);
                            //model.BlogPostDate = DateTime.ParseExact(hit["BlogPostDate"], "dd/MM/yyyy", null); 
                            BlogPostDate = Sitecore.DateUtil.IsoDateToDateTime(hit["BlogPostDate"]),
                            ID = hit["Id"]
                            //Author = hit.Document.Author
                        }); ;

                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception caught: {0}", e);
            }
            return View("~/Views/BlogProject/Postcard.cshtml",result);
            //return View("~/Views/Body/Body.cshtml",model);
        }

        public ActionResult BlogDetail(string ID)
        {



            Article model = new Article();

            try
            {

                var blog_item = Sitecore.Context.Database.GetItem(ID);
                List<string> tags = new List<string>();

                model.ShortDescription = blog_item["ShortDescription"];
                model.BlogTitle = blog_item["BlogTitle"];
                model.BlogCategory = blog_item["BlogCategory"];
                model.LongDescription = blog_item["LongDescription"];

                model.BlogPostDate = Sitecore.DateUtil.IsoDateToDateTime(blog_item["BlogPostDate"]);



                Sitecore.Data.Fields.ImageField imageField = blog_item.Fields["SmallImage"];
                Sitecore.Data.Fields.ImageField LargeimageField = blog_item.Fields["LargeImage"];
                Sitecore.Data.Fields.MultilistField tags_item = blog_item.Fields["Tags"];
                if (tags_item != null)
                {
                    foreach (Sitecore.Data.Items.Item taggs in tags_item.GetItems())
                    {
                        tags.Add(taggs.Name);
                    }
                }
                model.Tags = tags;
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
            }
            catch (Exception e) {
                Console.WriteLine("Exception caught: {0}", e);
            }


            return View("~/Views/BlogProject/BlogDetail.cshtml", model);

        }
    }
}