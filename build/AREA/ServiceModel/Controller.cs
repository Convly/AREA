using System.Collections.Generic;
using Network.NetTools;
using Network.Events;

namespace Service
{
    public delegate void ReactionDelegate(Event obj);
    public delegate void ActionDelegate(Event obj);

    public interface IController
    {
        ReactionDelegate Reaction(Network.Events.Event obj);
        void Action(Network.Events.Event obj);
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

        public ReactionDelegate Reaction(Network.Events.Event obj)
        {
            var action = (ServiceActionContent)obj.Data;

            return (_reactions[action.Name]);
        }

        public void Action(Network.Events.Event obj)
        {
            var action = (ServiceActionContent)obj.Data;

            _actions[action.Name](obj);
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
