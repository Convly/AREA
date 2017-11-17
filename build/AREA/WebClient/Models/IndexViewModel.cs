using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class IndexViewModel
    {
        public List<Area> Areas { get; set; }

        public IndexViewModel(List<Area> areas)
        {
            Areas = areas;
        }

        //TEMP
        public IndexViewModel()
        {
            Areas = new List<Area>
            {
                new Area("Emails"),
                new Area("FB notifications"),
                new Area("Twitter notifications")
            };
        }
    }
}