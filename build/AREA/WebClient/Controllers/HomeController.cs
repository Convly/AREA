using Network.NetTools;
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
        /// <summary>
        /// Send a tree for an <see cref="User"/> to the MongoDB
        /// </summary>
        /// <param name="treeJson">The entire tree data of the AREA serialized</param>
        /// <param name="treeIndex">The tree index to send to the MongoDB</param>
        /// <returns>The <see cref="IndexViewModel"/></returns>
        public ActionResult SendTree(string treeJson, int treeIndex)
        {
            string name = "";
            DataAccess db = DataAccess.Instance;
            if (User.Identity is ClaimsIdentity claimId)
            {
                name = claimId.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.SendTreeToUser(name, treeJson, treeIndex);
            }
            IndexViewModel vm = new IndexViewModel(name);
            return View(vm);
        }

        /// <summary>
        /// Add an new area for an <see cref="User"/> to the MongoDB
        /// </summary>
        /// <param name="areaName">The AREA's name to be created</param>
        /// <returns>A redirect to the "Index" action</returns>
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
        /// Update tokens for an <see cref="User"/> to the MongoDB
        /// </summary>
        /// <param name="tokensJson">The tokens of an <see cref="User"/> serialized</param>
        /// <param name="secretTokensJson">The secret tokens of an <see cref="User"/> serialized</param>
        /// <returns>A redirect to the "Index" action</returns>
        public ActionResult AddTokensForUser(string tokensJson, string secretTokensJson)
        {
            Dictionary<string, string> tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokensJson);
            Dictionary<string, string> secretTokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(secretTokensJson);

            string name = "";
            DataAccess db = DataAccess.Instance;
            if (User.Identity is ClaimsIdentity claimId)
            {
                name = claimId.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = db.GetUser(name);
                user.AccessToken = tokens;
                user.AccessTokenSecret = secretTokens;
                db.UpdateUserToken(user);
                Dispatcher.AddTokensAccess(user);
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