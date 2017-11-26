using System.Collections.Generic;
using Network.Events;
using Tweetinvi;
using Service;
using Network.NetTools;

namespace TwitterService
{
    public class TwitterController : Controller
    {
        private static string _consumerKeySecret = "SEr6QSvzRD05N9AzIpucxLT3MGUd3wZLnvqk8m7Lq8u26TFbQO";
        private static string _consumerKey = "5xQecQC24p9cTyGPQtiw7O0cE";

        public TwitterController(string name)
        {
            _name = name;
            _reactions = new Dictionary<string, ReactionDelegate>
            {
                { "ListenTweetFromAnyone", ListenTweetFromAnyone },
                { "ListenTweetFromMe", ListenTweetFromMe },
                { "ListenTweetFromAnyoneButMe", ListenTweetFromAnyoneButMe },
                { "ListenTweetWithKeyWord", ListenTweetWithKeyWord}
            };

            _actions = new Dictionary<string, ActionDelegate>
            {
                { "NewTweet", NewTweet }
            };
        }

        public void Authentification(Event obj)
        {
            if (obj.OwnerInfos.AccessToken.Count <= 0
                || obj.OwnerInfos.AccessTokenSecret.Count <= 0)
                return;
            var user = obj.OwnerInfos;
            string accessTokenSecret = user.AccessTokenSecret[_name];
            string accessToken = user.AccessToken[_name];

            Auth.SetUserCredentials(_consumerKey, _consumerKeySecret, accessToken, accessTokenSecret);
        }

        public void ListenTweetFromAnyone(Event obj)
        {
            var user = obj.OwnerInfos;
            var stream = Stream.CreateUserStream();

            Authentification(obj);
            stream.TweetCreatedByAnyone += (sender, args) =>
            {
                SendData(obj, new { resume = args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText });
            };
            stream.StartStream();
        }

        public void ListenTweetFromMe(Event obj)
        {
            var user = obj.OwnerInfos;
            var stream = Stream.CreateUserStream();

            Authentification(obj);
            stream.TweetCreatedByMe += (sender, args) =>
            {
                SendData(obj, new { resume = args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText });
            };
            stream.StartStream();
        }

        public void ListenTweetFromAnyoneButMe(Event obj)
        {
            var user = obj.OwnerInfos;
            var stream = Stream.CreateUserStream();

            Authentification(obj);
            stream.TweetCreatedByAnyoneButMe += (sender, args) =>
            {
                SendData(obj, new { resume = args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText });
            };
            stream.StartStream();
        }

        public void ListenTweetWithKeyWord(Event obj)
        {
            string regex = (string)obj.Data;
            var stream = Stream.CreateFilteredStream();

            Authentification(obj);
            stream.AddTrack(regex);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                SendData(obj, new { resume = "A tweet containing '" + regex + "' has been found : " + args.Tweet.FullText });
            };
        }

        public void NewTweet(Event obj)
        {
            Authentification(obj);
            var tmp = (ServiceActionContent)obj.Data;
            string text = (string)tmp.Args;
            Tweet.PublishTweet(text);
        }
    }
}
