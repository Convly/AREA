using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public ActionResult SendTree(string treeJson)
        {
            string name = "";
            DataAccess db = DataAccess.Instance;
            if (User.Identity is ClaimsIdentity claimId)
            {
                name = claimId.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.SendTreeToUser(name, treeJson);
            }
            IndexViewModel vm = new IndexViewModel(name);
            return View(vm);
        }

        public ActionResult AddArea(string areaName)
        {
            string name = "";
            DataAccess db = DataAccess.Instance;
            if (User.Identity is ClaimsIdentity claimId)
            {
                name = claimId.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.AddAreaToUser(name, areaName);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: Home/Index
        /// </summary>
        /// <returns>The <see cref="IndexViewModel"/></returns>
        public ActionResult Index()
        {
            string name = "";
            if (User.Identity is ClaimsIdentity claimId)
                name = claimId.FindFirst(ClaimTypes.NameIdentifier).Value;
            IndexViewModel vm = new IndexViewModel(name);
            return View(vm);
        }
    }
}