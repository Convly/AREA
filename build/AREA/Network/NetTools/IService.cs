using System;
using System.Collections.Generic;

namespace Network.NetTools
{
    /// <summary>
    /// The interface for a service
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Get the callback
        /// </summary>
        /// <returns>A Callback</returns>
        Func<Packet, int> GetCallback();

        /// <summary>
        /// Return the name of the API
        /// </summary>
        /// <returns></returns>
        string APIName();

        /// <summary>
        /// Get the list of actions
        /// </summary>
        /// <returns>A list of actions</returns>
        List<string> GetActionList();

        /// <summary>
        /// Get the list of reactions
        /// </summary>
        /// <returns>A list of reactions</returns>
        List<string> GetReactionList();

        /// <summary>
        /// Get the users list by action
        /// </summary>
        /// <param name="action">An action</param>
        /// <returns>A list of users</returns>
        List<User> GetUsersListByAction(string action);

        /// <summary>
        /// Get the users list by reaction
        /// </summary>
        /// <param name="reaction">An reaction</param>
        /// <returns>A list of users</returns>
        List<User> GetUsersListByReaction(string reaction);
    }
}
