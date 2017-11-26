
namespace Network.NetTools
{
    /// <summary>
    /// This class is used by a service to inform the Server that an action has been triggered and that a reaction has been created.
    /// </summary>
    public class ServiceReactionContent
    {
        private string _name;
        private User _user;
        private object _reactionContent;
        private string _service;

        /// <summary>
        /// Return the name of the action to execute.
        /// </summary>
        public string Name { get => _name; }

        /// <summary>
        /// Return the args needed to the launch the action function.
        /// </summary>
        public User User { get => _user; }

        /// <summary>
        /// Return an object containing some content about the reaction.
        /// </summary>
        public object ReactionContent { get => _reactionContent; }

        /// <summary>
        /// Return the name of the service.
        /// </summary>
        public object Service { get => _service; }

        /// <summary>
        /// Constructor of the ServiceActionContent class.
        /// </summary>
        /// <param name="name">The name of the reaction.</param>
        /// <param name="user">Informations about the user.</param>
        /// <param name="reactionContent">Contents about the reaction.</param>
        /// <param name="service">The name of the service.</param>
        public ServiceReactionContent(string name, User user, object reactionContent, string service)
        {
            _name = name;
            _user = user;
            _reactionContent = reactionContent;
            _service = service;
        }
    }
}
