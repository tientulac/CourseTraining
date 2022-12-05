using CourseTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseTraining.Controllers
{
    public class TrainerPageController : Controller
    {
        // GET: TrainerPage
        private LinqDataContext db = new LinqDataContext();

        public ActionResult Index()
        {
            return View();
        }
    }
}