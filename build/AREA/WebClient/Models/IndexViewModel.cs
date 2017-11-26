using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Network;
using Network.NetTools;

namespace WebClient.Models
{
    /// <summary>
    /// Defines an <see cref="IndexViewModel"/>
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// The list of AREAs visible
        /// </summary>
        public List<ATreeRoot> Areas;

        /// <summary>
        /// The list of <see cref="Service"/>
        /// </summary>
        public List<Service> Services;

        /// <summary>
        /// The current <see cref="User"/>
        /// </summary>
        public User CurrentUser;

        /// <summary>
        /// Serialize the list of AREAs
        /// </summary>
        /// <returns>The stringified list of AREAs</returns>
        public string AreasToJSON()
        {
            return (JsonConvert.SerializeObject(Areas));
        }

        /// <summary>
        /// Serialize the list of <see cref="Service"/>
        /// </summary>
        /// <returns>The stringified list of <see cref="Service"/></returns>
        public string ServicesToJSON()
        {
            return (JsonConvert.SerializeObject(Services));
        }

        /// <summary>
        /// Constructor of the <see cref="IndexViewModel"/>
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        public IndexViewModel(string email)
        {
            DataAccess db = DataAccess.Instance;
            CurrentUser = db.GetUser(email);
            AreaTree tree = db.GetAreas(email);
            Areas = tree.AreasList;
            Services = Dispatcher.GetAvailableServices(CurrentUser);
        }
    }
}