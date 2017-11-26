using System.Web;
using System.Web.Mvc;

namespace WebClient
{
    /// <summary>
    /// Configures all application filters
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Configures all application filters
        /// </summary>
        /// <param name="filters">The global filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
