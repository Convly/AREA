using System.Collections.Generic;

namespace Service
{
    public delegate void ReactionDelegate(object obj);
    public delegate void ActionDelegate();

    public interface IController
    {
        ReactionDelegate Reaction(string name);
        void Action(string name);
        List<string> GetActions();
        List<string> GetReactions();
    }

    class Controller : IController
    {
        private Dictionary<string, ReactionDelegate>    _reactions;
        private Dictionary<string, ActionDelegate>      _actions;

        public Controller()
        {
            _reactions = new Dictionary<string, ReactionDelegate>();
            _actions = new Dictionary<string, ActionDelegate>();
        }

        public ReactionDelegate Reaction(string name)
        {
            return (_reactions[name]);
        }

        public void Action(string name)
        {
            _actions[name]();
        }

        public List<string> GetActions()
        {
            List<string> tmp = new List<string>();

            foreach (var it in _actions)
                tmp.Add(it.Key);
            return (tmp);
        }

        public List<string> GetReactions()
        {
            List<string> tmp = new List<string>();

            foreach (var it in _reactions)
                tmp.Add(it.Key);
            return (tmp);
        }
    }
}
