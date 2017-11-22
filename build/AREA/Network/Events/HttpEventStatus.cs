namespace Network.Events
{
    /// <summary>
    /// Defines a data model used to store an http status
    /// </summary>
    public class HttpEventStatus
    {
        /// <summary>
        /// Code of the status
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Message of the status
        /// </summary>
        public string Message { get; set; }
    }
}
