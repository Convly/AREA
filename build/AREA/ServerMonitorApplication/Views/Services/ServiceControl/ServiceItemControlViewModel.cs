namespace ServerMonitorApplication
{
    /// <summary>
    /// All service properties
    /// </summary>
    public class ServiceItemControlViewModel
    {
        #region Properties

        /// <summary>
        /// The display service name of the list
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// State of the service
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// First character of the service name
        /// </summary>
        public string Label { get; set; }

        #endregion
    }
}
