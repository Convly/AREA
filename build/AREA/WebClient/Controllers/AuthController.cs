using Microsoft.AspNet.Identity;
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
    /// The Auth Controller
    /// </summary>
    public class AuthController : Controller
    {
        /// <summary>
        /// GET: Auth/Login
        /// </summary>
        /// <returns>The 'Login' view</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        /// <summary>
        /// GET: Auth/Login?ReturnUrl=
        /// </summary>
        /// <param name="model">A LoginViewModel</param>
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
                ModelState.AddModelError(string.Empty, "Le nom d'utilisateur ou le mot de passe est incorrect.");
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
        /// <param name="login">The user's login</param>
        /// <param name="password">The user's password</param>
        /// <returns>User was found</returns>
        private bool ValidateUser(string login, string password)
        {
            // TODO : insérer ici la validation des identifiant et mot de passe de l'utilisateur...

            // Pour ce tutoriel, j'utilise une validation extrêmement sécurisée...
            return (login == password);
        }

        /// <summary>
        /// GET: Auth/Register?ReturnUrl=
        /// </summary>
        /// <param name="model">A LoginViewModel</param>
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
            if (!ValidateUser(model.Password, model.ConfirmPassword))
            {
                ModelState.AddModelError(string.Empty, "Les mots de passes diffèrent.");
                model.Password = "";
                model.ConfirmPassword = "";
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