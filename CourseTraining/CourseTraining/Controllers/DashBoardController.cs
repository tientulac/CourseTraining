using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class DashBoardController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}