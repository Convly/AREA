namespace ServerMonitorApplication
{
    /// <summary>
    /// The design-time data for a <see cref="ServiceItemControlViewModel"/>
    /// </summary>
    public class ServiceItemControlModel : ServiceItemControlViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the model
        /// </summary>
        public static ServiceItemControlModel Instance => new ServiceItemControlModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServiceItemControlModel()
        {
            ServiceName = "Twitter";
            IsActive = true;
            Label = "T";
        }

        #endregion
    }
}
