using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetTunnel
{
    /// <summary>
    /// Manages users.
    /// </summary>
    public class UserManager
    {
        private Dictionary<ulong, User> _users = new Dictionary<ulong, User>();
        private readonly User _local_user;

        /// <param name="local_user">The user running this application.</param>
        public UserManager(User local_user)
        {
            _local_user = local_user;
        }

        /// <summary>
        /// The users this manager is managing.
        /// </summary>
        public ICollection<User> users { get { return _users.Values; } }        

        /// <summary>
        /// The user running this instance.
        /// </summary>
        public User local_user { get { return _local_user; } }

        /// <summary>
        /// Finds a user by remote_userid.
        /// </summary>
        /// <returns>The user or null if none found.</returns>
        public User FindUserID(ulong userid)
        {
            User user;
            _users.TryGetValue(userid, out user);         
            return user;
        }

        /// <summary>
        /// Adds a user to the manager.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void Add(User user)
        {
            _users.Add(user.userid, user);
        }

        /// <summary>
        /// Removes a user from the manager.
        /// </summary>
        /// <param name="remote_userid">The remote_userid to remove.</param>
        public void Remove(ulong userid)
        {
            _users.Remove(userid);
        }        
    }
}
