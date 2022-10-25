using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class CourseTrainerController : Controller
    {
        // GET: CourseTrainer
        public ActionResult Index()
        {
            return View();
        }
    }
}