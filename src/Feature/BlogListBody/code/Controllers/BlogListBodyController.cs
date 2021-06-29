using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogProject.Feature.BlogListBody.Controllers
{
    public class BlogListBodyController : Controller
    {
        // GET: BlogListBody
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BlogListBody() {

            return View();
        }
    }
}