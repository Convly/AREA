namespace ServerMonitorApplication
{
    public class ServiceLoggerItemControlViewModel
    {
        #region Properties
        
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Content (service state)
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Time of the action
        /// </summary>
        public string Time { get; set; }

        #endregion
    }
}
