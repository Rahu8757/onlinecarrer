using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onlinecarrer.Controllers
{
    public class welcomeController : Controller
    {
        // GET: welcome
        public ActionResult Index()
        {
            return View();
        }
    }
}