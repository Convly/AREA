using System.Collections.Generic;

namespace Network.NetTools
{
    public interface IService
    {
        string APIName();
        List<string> GetActionList();
        List<string> GetReactionList();
        List<User> GetUsersListByAction(string action);
        List<User> GetUsersListByReaction(string reaction);
    }
}
