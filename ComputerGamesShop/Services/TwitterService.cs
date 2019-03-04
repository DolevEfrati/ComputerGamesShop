using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace ComputerGamesShop.Services
{
    public class Twitter
    {
        private TwitterContext twitterContext;

        public Twitter()
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "5qiNFPZ2vNqSbto93iC32VTGm",
                    ConsumerSecret = "YryJpYO8TU9dwrEufQzQDhQbKlUZS4dHR9oXcYk69b2I05X7B3",
                    AccessToken = "1102170270649982978-bcHFljggRFO81KQKdFfd5Ni6fz3z9a",
                    AccessTokenSecret = "h4cT5DYsvQLr86mE2VVjqvOq7VQxgK6y1SDXyeRCZfBOg"
                }
            };

            twitterContext = new TwitterContext(auth); ;

        }

        public async Task Tweet(string message)
        {
            var status = await twitterContext.TweetAsync(message);
        }
    }
}