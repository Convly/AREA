﻿using System.Web;
using System.Web.Optimization;
using WebClient.Models;

namespace WebClient
{
    /// <summary>
    /// Loads all scripts and style files into the application
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Load all scripts and style files
        /// </summary>
        /// <param name="bundles">The bundle collections</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            DataAccess db = DataAccess.Instance;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Scripts/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Scripts/tree.js"));
        }
    }
}
