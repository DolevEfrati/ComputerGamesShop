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
        public static JObject connectedUser = new JObject();
        private static List<int> cartList = new List<int>();
        private static List<Store> storesList = new List<Store>();


        public static void setConnectedUser(ISession session)
        {
            connectedUser = JObject.Parse(session.GetString(USER_SESSION_KEY));
        }

        public static void removeUser(ISession session)
        {
            connectedUser = new JObject();
        }

        public static JObject getConnectedUser()
        {
            return connectedUser;
        }

        public static JObject getConnectedUser(ISession session)
        {
            return connectedUser;
        }

        public static bool isConnectedUser(ISession session)
        {
            return (session.GetString(USER_SESSION_KEY) != null);
        }

        public static bool isManagerConnected(ISession session)
        {
            if (isConnectedUser(session))
            {
                return (session.GetString(IS_MANAGER_SESSION_KEY).ToLower() == "true");
            }
            return false;
        }

        public static void addToCart(int gameId)
        {
            cartList.Add(gameId);
        }

        public static void deleteFromCart(int gameId)
        {
            cartList.Remove(gameId);
        }

        public static void clearCart()
        {
            cartList.Clear();
        }

        public static List<int> getCartList()
        {
            return cartList;
        }

        public static bool isGameInCart(int gameID)
        {
            return cartList.Contains(gameID);
        }

        public static void setStores(List<Store> list)
        {
            storesList = list;
        }

        public static List<Store> getStores()
        {
            return storesList;
        }
    }
}
