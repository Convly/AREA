using System.Collections.Generic;

namespace ServerMonitorApplication
{
    /// <summary>
    /// A view model for the overview service list
    /// </summary>
    public class ListServiceItemControlViewModel
    {
        #region Properties

        /// <summary>
        /// The service list items for the list
        /// </summary>
        public List<ServiceItemControlViewModel> Items { get; set; }

        #endregion
    }
}
