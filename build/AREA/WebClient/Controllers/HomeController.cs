using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// The Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        IndexViewModel vm = new IndexViewModel();

        /// <summary>
        /// GET: Home/Index
        /// </summary>
        /// <returns>The <see cref="IndexViewModel"/></returns>
        public ActionResult Index()
        {
            return View(vm);
        }
    }
}