using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        IndexViewModel vm = new IndexViewModel();

        // GET: Home
        public ActionResult Index()
        {
            return View(vm);
        }
    }
}