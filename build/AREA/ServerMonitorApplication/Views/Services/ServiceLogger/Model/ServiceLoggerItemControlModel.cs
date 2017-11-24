namespace ServerMonitorApplication
{
    public class ServiceLoggerItemControlModel : ServiceLoggerItemControlViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of <see cref="ServiceLoggerItemControlModel"/>
        /// </summary>
        public static ServiceLoggerItemControlModel Instance => new ServiceLoggerItemControlModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceLoggerItemControlModel()
        {
            Username = "Micro";
            Content = "Facebook desable";
            Time = "11:54pm";
        }

        #endregion
    }
}
