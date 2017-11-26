
namespace Network.NetTools
{
    /// <summary>
    /// This class is used by the server to ask a service to execute an action, or link a reaction.
    /// </summary>
    public class ServiceActionContent
    {
        private string _name;
        private User _user;
        private object _args;

        /// <summary>
        /// Return the name of the action to execute.
        /// </summary>
        public string Name { get => _name; }

        /// <summary>
        /// 
        /// </summary>
        public User User { get => _user; }

        /// <summary>
        /// Return the args needed to the launch the action function.
        /// </summary>
        public object Args { get => _args; }

        /// <summary>
        /// Constructor of the ServiceActionContent class.
        /// </summary>
        /// <param name="name">The name of the <see cref="ServiceActionContent"/></param>
        /// <param name="user">An user</param>
        /// <param name="args">The <see cref="ServiceActionContent"/>'s arguments</param>
        public ServiceActionContent(string name, User user, object args)
        {
            _name = name;
            _user = user;
            _args = args;
        }
    }
}
