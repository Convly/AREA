using System.Collections.Generic;

namespace ServerMonitorApplication
{
    /// <summary>
    /// A view model for the overview list
    /// </summary>
    public class ListServiceLoggerItemControlViewModel
    {
        #region Properties

        /// <summary>
        /// The service logger list
        /// </summary>
        public static List<ServiceLoggerItemControlViewModel> Items { get; set; }

        #endregion
    }
}
