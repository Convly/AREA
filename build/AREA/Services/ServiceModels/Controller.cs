using static Network.Events.AddUserEvent;
using System.Collections.Generic;
using Network.NetTools;
using Network.Events;
using System;

namespace Service
{
    public delegate void ReactionDelegate(ReactionRegisterContent obj);
    public delegate void ActionDelegate(ServiceActionContent obj);

    public interface IController
    {
        ReactionDelegate Reaction(ReactionRegisterContent obj);
        void Action(ServiceActionContent obj);
        List<string> GetActionList();
        List<string> GetReactionList();
    }

    public class Controller : IController
    {
        protected Dictionary<string, ReactionDelegate> _reactions;
        protected Dictionary<string, ActionDelegate> _actions;
        protected string _name;

        public Controller()
        {
            _reactions = new Dictionary<string, ReactionDelegate>();
            _actions = new Dictionary<string, ActionDelegate>();
        }

        public void SendData(ReactionRegisterContent obj, object reactionContent)
        {
            Network.NetTools.User user = obj.Owner;
            ServiceReactionContent data = new ServiceReactionContent(obj.ReactionName, user, reactionContent, _name);
            Event react = new TriggerReactionEvent(HttpEventSource.SERVICE, HttpEventType.COMMAND, user, data);
            Packet packet = new Packet(_name, PacketCommand.REACTION, react);

            Network.Client.Instance.SendDataToServer(packet);
            Console.WriteLine(reactionContent.ToString());
        }

        public ReactionDelegate Reaction(ReactionRegisterContent obj)
        {
            return (_reactions[obj.ReactionName]);
        }

        public void Action(ServiceActionContent obj)
        {
            _actions[obj.Name](obj);
        }

        public List<string> GetActionList()
        {
            List<string> tmp = new List<string>();

            foreach (var it in _actions)
                tmp.Add(it.Key);
            return (tmp);
        }

        public List<string> GetReactionList()
        {
            List<string> tmp = new List<string>();

            foreach (var it in _reactions)
                tmp.Add(it.Key);
            return (tmp);
        }
    }
}
