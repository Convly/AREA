using Microsoft.AspNet.Identity;
using Network.NetTools;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebClient.Models;

namespace WebClient.Controllers
{
    /// <summary>
    /// The Auth Controller
    /// </summary>
    public class AuthController : Controller
    {
        /// <summary>
        /// GET: Auth/Login
        /// </summary>
        /// <returns>The <see cref="LoginViewModel"/></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        /// <summary>
        /// POST: Auth/Login?ReturnUrl=
        /// </summary>
        /// <param name="model">A <see cref="LoginViewModel"/></param>
        /// <param name="returnUrl">Parameters for the ReturnUrl</param>
        /// <returns>An action</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!ValidateUser(model.Email, model.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect Email or Password.");
                return View(model);
            }

            // L'authentification est réussie, 
            // injecter l'identifiant utilisateur dans le cookie d'authentification :
            var loginClaim = new Claim(ClaimTypes.NameIdentifier, model.Email);
            var claimsIdentity = new ClaimsIdentity(new[] { loginClaim }, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(claimsIdentity);

            // Rediriger vers l'URL d'origine :
            if (Url.IsLocalUrl(ViewBag.ReturnUrl))
                return Redirect(ViewBag.ReturnUrl);
            // Par défaut, rediriger vers la page d'accueil :
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Check if the login / password combination matches the database
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="pwd">The user's password</param>
        /// <returns>User was found</returns>
        private bool ValidateUser(string email, string pwd)
        {
            // Temp
            //return true;

            // Check if user authentification is correct
            try
            {
                DataAccess da = DataAccess.Instance;
                var user = da.GetUser(email);
                if (GetSha256FromString(pwd) != user.Pwd)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Hash a string into an SHA-256 encoded string
        /// </summary>
        /// <param name="strData">The string to be encoded</param>
        /// <returns>The SHA-256 encoded string</returns>
        public static string GetSha256FromString(string strData)
        {
            var message = Encoding.ASCII.GetBytes(strData);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        /// <summary>
        /// POST: Auth/Register?ReturnUrl=
        /// </summary>
        /// <param name="model">A <see cref="LoginViewModel"/></param>
        /// <param name="returnUrl">Parameters for the ReturnUrl</param>
        /// <returns>An action</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginViewModel model, string returnUrl)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords are not the same.");
                model.Password = "";
                model.ConfirmPassword = "";
                return View("Login", model);
            }

            // Check email validity
            if (!model.Email.Contains('@'))
            {
                ModelState.AddModelError(string.Empty, "Email incorrect.");
                return View("Login", model);
            }
            DataAccess da = DataAccess.Instance;
            var users = da.GetUsers();
            foreach (var user in users)
            {
                if (user.Email == model.Email)
                {
                    ModelState.AddModelError(string.Empty, "Email already used.");
                    return View("Login", model);
                }
            }

            // Add user to db
            if (!da.Create(new User(model.Email, GetSha256FromString(model.Password))))
            {
                return View("Login", model);
            }

            // L'authentification est réussie, 
            // injecter l'identifiant utilisateur dans le cookie d'authentification :
            var loginClaim = new Claim(ClaimTypes.NameIdentifier, model.Email);
            var claimsIdentity = new ClaimsIdentity(new[] { loginClaim }, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(claimsIdentity);

            // Rediriger vers l'URL d'origine :
            if (Url.IsLocalUrl(ViewBag.ReturnUrl))
                return Redirect(ViewBag.ReturnUrl);
            // Par défaut, rediriger vers la page d'accueil :
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Log out the user
        /// </summary>
        /// <returns>A redirection to the login page</returns>
        [HttpGet]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();

            // Rediriger vers la page d'accueil :
            return RedirectToAction("Login", "Auth");
        }
    }
}