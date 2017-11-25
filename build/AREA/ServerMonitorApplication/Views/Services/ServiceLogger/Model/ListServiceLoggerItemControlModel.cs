using System.Collections.Generic;

namespace ServerMonitorApplication
{
    public class ListServiceLoggerItemControlModel : ListServiceLoggerItemControlViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of <see cref="ListServiceLoggerItemControlModel"/>
        /// </summary>
        public static ListServiceLoggerItemControlModel Instance => new ListServiceLoggerItemControlModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ListServiceLoggerItemControlModel()
        {
            Items = new List<ServiceLoggerItemControlViewModel>
            {
                new ServiceLoggerItemControlViewModel
                {
                    Username = "Micro",
                    Content = "Facebook service activated",
                    Time = "11:54pm"
                },
                new ServiceLoggerItemControlViewModel
                {
                    Username = "Alibabouche",
                    Content = "Twitter service activated",
                    Time = "08:17am"
                },
                new ServiceLoggerItemControlViewModel
                {
                    Username = "Alibabouche",
                    Content = "Gmail service activated",
                    Time = "09:53am"
                },
                new ServiceLoggerItemControlViewModel
                {
                    Username = "Fanfan la tulipe",
                    Content = "Facebook service desabled",
                    Time = "00:01am"
                },
            };
        }

        #endregion
    }
}
