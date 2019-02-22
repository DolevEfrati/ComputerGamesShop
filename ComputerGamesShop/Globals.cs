using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ComputerGamesShop.Models;
using Newtonsoft.Json.Linq;

namespace ComputerGamesShop
{
    public static class Globals
    {
        public const string USER_SESSION_KEY = "user";
        public const string IS_MANAGER_SESSION_KEY = "isManager";

        public static JObject getConnectedUser(ISession session)
        {
            return JObject.Parse(session.GetString(USER_SESSION_KEY));
        }

        public static bool isConnectedUser(ISession session)
        {
            return (session.GetString(USER_SESSION_KEY) != null);
        }

        public static bool isManagerConnected(ISession session)
        {
            return (session.GetString(IS_MANAGER_SESSION_KEY).ToLower() == "true");
        }
    }
}
