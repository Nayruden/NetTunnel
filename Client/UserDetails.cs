using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetTunnel
{
    // PORT: Don't both storing this, just store the last packet...
    class UserDetails
    {
        public static UserDetails local_user = new UserDetails();
        private static List<UserDetails> users = new List<UserDetails>();

        public string nick;
        public ulong userid = 0;
        public List<Service> services = new List<Service>();

        public UserMessage toUserMessage()
        {
            return toUserMessage(MessageState.Modified);
        }

        public UserMessage toUserMessage( MessageState state )
        {
            var user_message = new UserMessage(nick, userid);
            user_message.state = state;
            user_message.services = services;

            return user_message;
        }

        public static UserDetails getByUserid(ulong userid)
        {
            if (local_user.userid == userid)
                return local_user;

            return users.SingleOrDefault(u => u.userid == userid);
        }

        public static void add( UserDetails user )
        {
            users.Add(user);
        }

        public static void remove(ulong userid)
        {
            users.Remove(getByUserid(userid));
        }
    }
}
