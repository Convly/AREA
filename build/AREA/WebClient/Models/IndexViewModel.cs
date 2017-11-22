using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

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
        public List<Area> Areas { get; set; }

        /// <summary>
        /// Serialize the list of <see cref="Area"/>
        /// </summary>
        /// <returns>The stringified list of AREAs</returns>
        public string AreasToJSON()
        {
            return (JsonConvert.SerializeObject(Areas));
        }

        /// <summary>
        /// Get the selected <see cref="Area"/>
        /// </summary>
        /// <returns>The instance of the selected <see cref="Area"/></returns>
        public Area GetSelectedArea()
        {
            for (int i = 0; i < Areas.Count; i++)
            {
                if (Areas[i].IsSelected)
                    return (Areas[i]);
            }
            return (null);
        }

        /// <summary>
        /// Get the selected <see cref="Area"/> index
        /// </summary>
        /// <returns>The instance of the selected <see cref="Area"/> index</returns>
        public int GetSelectedAreaIndex()
        {
            for (int i = 0; i < Areas.Count; i++)
            {
                if (Areas[i].IsSelected)
                    return (i);
            }
            return (-1);
        }

        public IndexViewModel(string email)
        {
            DataAccess db = DataAccess.Instance;
            User user = db.GetUser(email);
            Areas = user.AreasList;
        }
    }
}