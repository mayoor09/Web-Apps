using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Zoo()
        {
            return RedirectToAction("DisplayZoo", "Zoo");
        }

        public ActionResult Animals()
        {
            return RedirectToAction("DisplayAnimals", "Animals");
        }


        public String Creat()
        {
            return ("this is create method");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details()
        {
            ViewBag.Message = "Your Details page.";

            return View();
        }
    }
}