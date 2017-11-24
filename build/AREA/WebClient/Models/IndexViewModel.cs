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
        /// The list of <see cref="Area"/> visible
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
        /// Serialize the list of <see cref="Area"/>
        /// </summary>
        /// <returns>The stringified list of AREAs</returns>
        public string AreasToJSON()
        {
            return (JsonConvert.SerializeObject(Areas));
        }

        public string ServicesToJSON()
        {
            return (JsonConvert.SerializeObject(Services));
        }

        public IndexViewModel(string email)
        {
            DataAccess db = DataAccess.Instance;
            CurrentUser = db.GetUser(email);
            AreaTree tree = db.GetAreas(email);
            Areas = tree.AreasList;
            //Services = Dispatcher.GetAvailableServices(CurrentUser);
            Services = new List<Service>()
            {
                new Service("Facebook", false, new Dictionary<string, ServiceType>(), new Dictionary<string, ServiceType>()),
                new Service("Instagram", false, new Dictionary<string, ServiceType>(), new Dictionary<string, ServiceType>()),
                new Service("Twitter", true, new Dictionary<string, ServiceType>(), new Dictionary<string, ServiceType>())
            };
        }
    }
}