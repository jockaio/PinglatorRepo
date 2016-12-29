using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinglator.Controllers
{
    public class ComponentsController : Controller
    {
        // GET: Components
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InputForm()
        {
            return View("InputForm");
        }
    }
}