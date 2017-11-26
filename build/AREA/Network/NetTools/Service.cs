using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.NetTools
{
    /// <summary>
    /// An <see cref="Enum"/> defining a service type
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// The service is an action
        /// </summary>
        ACTION,
        /// <summary>
        /// The service is an reaction
        /// </summary>
        REACTION
    }

    /// <summary>
    /// Service like (Facebook, Twitter, Slack...)
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Constructor for a <see cref="Service"/>
        /// </summary>
        /// <param name="name_">The <see cref="Service"/>'s name</param>
        /// <param name="accessTokenSecret_">The <see cref="Service"/> needs an AccessTokenSecret</param>
        /// <param name="actions_">The <see cref="Service"/>'s actions</param>
        /// <param name="reactions_">The <see cref="Service"/>'s reactions</param>
        /// <param name="infos_">The <see cref="Service"/>'s infos client</param>
        public Service(string name_, bool accessTokenSecret_, Dictionary<string, ServiceType> actions_, Dictionary<string, ServiceType> reactions_, InfosClient infos_)
        {
            name = name_;
            accessTokenSecret = accessTokenSecret_;
            actions = actions_;
            reactions = reactions_;
            infos = infos_;
        }

        private InfosClient infos;

        private string name;

        /// <summary>
        /// The <see cref="Service"/>'s name
        /// </summary>
        public string Name { get => name; set => name = value; }

        private bool accessTokenSecret = false;

        /// <summary>
        /// The <see cref="Service"/> needs an AccessTokenSecret
        /// </summary>
        public bool AccessTokenSecret { get => accessTokenSecret; set => accessTokenSecret = value; }

        private Dictionary<string, ServiceType> actions;

        /// <summary>
        /// The <see cref="Service"/>'s actions
        /// </summary>
        public Dictionary<string, ServiceType> Actions { get => actions; set => actions = value; }

        private Dictionary<string, ServiceType> reactions;

        /// <summary>
        /// The <see cref="Service"/>'s reactions
        /// </summary>
        public Dictionary<string, ServiceType> Reactions { get => reactions; set => reactions = value; }

        /// <summary>
        /// The <see cref="Service"/>'s infos client
        /// </summary>
        public InfosClient Infos { get => infos; set => infos = value; }
    }
}
