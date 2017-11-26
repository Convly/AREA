using Network.NetTools;
using System.Collections.Generic;

namespace Service
{
    public delegate void ReactionDelegate(object obj);
    public delegate void ActionDelegate(object obj);

    public interface IController
    {
        ReactionDelegate Reaction(string name);
        void Action(string name, object obj);
        List<string> GetActionList();
        List<string> GetReactionList();
    }

    public class Controller : IController
    {
        protected Dictionary<string, ReactionDelegate> _reactions;
        protected Dictionary<string, ActionDelegate> _actions;

        public Controller()
        {
            _reactions = new Dictionary<string, ReactionDelegate>();
            _actions = new Dictionary<string, ActionDelegate>();
        }

        public ReactionDelegate Reaction(string name)
        {
            return (_reactions[name]);
        }

        public void Action(string name, object obj)
        {
            _actions[name](obj);
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
