
namespace Network.NetTools
{
    /// <summary>
    /// This class is used by the server to ask a service to execute an action, or link a reaction.
    /// </summary>
    public class ServiceActionContent
    {
        private string _name;
        private object _args;

        /// <summary>
        /// Return the name of the action to execute.
        /// </summary>
        public string Name { get => _name; }

        /// <summary>
        /// Return the args needed to the launch the action function.
        /// </summary>
        public object Args { get => _args; }

        /// <summary>
        /// Constructor of the ServiceActionContent class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public ServiceActionContent(string name, object args)
        {
            _name = name;
            _args = args;
        }
    }
}
