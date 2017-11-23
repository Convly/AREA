using System.Collections.Generic;

namespace ServerMonitorApplication
{
    /// <summary>
    /// The design-time data for a <see cref="ListServiceItemControlViewModel"/>
    /// </summary>
    public class ListServiceItemControlModel : ListServiceItemControlViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the model
        /// </summary>
        public static ListServiceItemControlModel Instance => new ListServiceItemControlModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Defautl constructor
        /// </summary>
        public ListServiceItemControlModel()
        {
            Items = new List<ServiceItemControlViewModel>
            {
                new ServiceItemControlViewModel
                {
                    ServiceName = "Twitter",
                    IsActive = true,
                    Label = "ON"

                },
                new ServiceItemControlViewModel
                {
                    ServiceName = "Facebook",
                    IsActive = true,
                    Label = "OFF"
                },
                new ServiceItemControlViewModel
                {
                    ServiceName = "Gmail",
                    IsActive = true,
                    Label = "ON"
                },
            };
        }

        #endregion
    }
}
