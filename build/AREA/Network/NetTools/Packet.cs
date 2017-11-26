using System;
using System.Collections.Generic;

namespace Network.NetTools
{
    /// <summary>
    /// Enum used to defined the type of the <see cref="Packet"/>'s <see cref="Object"/>
    /// </summary>
    public enum PacketCommand
    {
        /// <summary>
        /// Communication direction: Client => Server.  
        /// Request a register from a Client to the Server.  
        /// The object associated to the command must be a <see cref="string"/> (which is the server's password).  
        /// </summary>
        C_REGISTER,
        C_QUIT,
        C_UNLOCK,
        C_PING,
        S_LOGIN_SUCCESS,
        S_PONG,
        S_DISCONNECT,
        S_EVENT,
        /// <summary>
        /// Communication direction: Server => Client.
        /// Ask the client to execute an action.
        /// The object associated to the command must be an <see cref="Network.Events.Event"/> (which is the action to execute and his paramaters).
        /// </summary>
        ACTION,
        /// <summary>
        /// Communication direction: Server => Client.
        /// Ask the client to link a reaction to an user.
        /// The object associated to the command must be an <see cref="Network.Events.Event"/> (which is the reaction to launch and his paramaters).
        /// </summary>
        REACTION_REGISTER,
        /// <summary>
        /// Communication direction: Client => Server.
        /// Send to the message bus the fact that an reaction has been triggered.
        /// The object associated to the command must be an ReactionRegisterContent (which is the reaction and the user link to a reaction).
        /// </summary>
        REACTION,
        /// <summary>
        /// Communication direction: Both.  
        /// Inform a network entity about an error that occured.  
        /// The object associated to the command must be a <see cref="string"/>.  
        /// </summary>
        ERROR,

        S_ENABLE,
        S_DISABLE
    }

    /// <summary>
    /// Class used to transfer data across the network.
    /// </summary>
    public class Packet
    {
        private string name;
        private uint key;
        private KeyValuePair<PacketCommand, Object> data;


        /// <summary>
        /// Default constructor for a <see cref="Packet"/>
        /// </summary>
        public Packet()
        {
        }

        /// <summary>
        /// Main constructor for client side. The key is initialized to 0 and the registration state to false.
        /// </summary>
        /// <param name="name_">The name of the emitter.</param>
        /// <param name="command_">The command associated to <paramref name="data_"/></param>
        /// <param name="data_">The <see cref="Object"/> which contain the data</param>
        public Packet(string name_, PacketCommand command_, Object data_)
        {
            this.Name = name_;
            this.Key = 0;
            this.Data = new KeyValuePair<PacketCommand, Object>(command_, data_);
        }

        /// <summary>
        /// Main constructor for server side, the key must be initialized with a value.
        /// </summary>
        /// <param name="name_">The name of the emitter. For server side, it will be "root"</param>
        /// <param name="key_">The value of the Locker key</param>
        /// <param name="command_">The command associated to <paramref name="data_"/></param>
        /// <param name="data_">The <see cref="Object"/> which contain the data</param>
        public Packet(string name_, uint key_, PacketCommand command_, Object data_)
        {
            this.Name = name_;
            this.Key = key_;
            this.Data = new KeyValuePair<PacketCommand, Object>(command_, data_);
        }

        /// <summary>
        /// Access the name of the packet's author
        /// </summary>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Access the <see cref="Lock.Locker"/>'s key of the <see cref="Packet"/>
        /// </summary>
        public uint Key { get => key; set => key = value; }
        /// <summary>
        /// Access the Data of the <see cref="Packet"/>
        /// </summary>
        public KeyValuePair<PacketCommand, object> Data { get => data; set => data = value; }
    }
}