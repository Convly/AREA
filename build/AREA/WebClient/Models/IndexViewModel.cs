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
        public List<ATreeRoot> Areas { get; set; }

        /// <summary>
        /// Serialize the list of <see cref="Area"/>
        /// </summary>
        /// <returns>The stringified list of AREAs</returns>
        public string AreasToJSON()
        {
            return (JsonConvert.SerializeObject(Areas));
        }

        public IndexViewModel(string email)
        {
            DataAccess db = DataAccess.Instance;
            User user = db.GetUser(email);
            AreaTree tree = db.GetAreas(email);
            Areas = tree.AreasList;
        }
    }
}