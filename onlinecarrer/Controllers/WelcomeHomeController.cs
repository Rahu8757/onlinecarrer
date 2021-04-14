using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onlinecarrer.Controllers
{
    public class WelcomeHomeController : Controller
    {
        // GET: WelcomeHome
        public ActionResult Welcome()
        {
            return View();
        }
        
    }
}