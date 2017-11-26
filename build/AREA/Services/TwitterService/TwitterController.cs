using System.Collections.Generic;
using Network.Events;
using Tweetinvi;
using Service;
using Network.NetTools;
using System;
using Newtonsoft.Json;

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

        public void Authentification(ReactionRegisterContent obj)
        {
            if (obj.Owner.AccessToken.Count <= 0
                || obj.Owner.AccessTokenSecret.Count <= 0)
                return;
            var user = obj.Owner;
            string accessTokenSecret = user.AccessTokenSecret[_name];
            string accessToken = user.AccessToken[_name];

            Auth.SetUserCredentials(_consumerKey, _consumerKeySecret, accessToken, accessTokenSecret);
        }

        public void Authentification(ServiceActionContent obj)
        {
            if (obj.User.AccessToken.Count <= 0
                || obj.User.AccessTokenSecret.Count <= 0)
                return;
            var user = obj.User;
            string accessTokenSecret = user.AccessTokenSecret[_name];
            string accessToken = user.AccessToken[_name];

            Auth.SetUserCredentials(_consumerKey, _consumerKeySecret, accessToken, accessTokenSecret);
        }

        public void ListenTweetFromAnyone(ReactionRegisterContent obj)
        {
            Console.WriteLine("Yolo1ListenAll");
            var user = obj.Owner;
            Authentification(obj);

            var stream = Stream.CreateUserStream();

            stream.TweetCreatedByAnyone += (sender, args) =>
            {
                Console.WriteLine(args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
                SendData(obj, args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
            };
            stream.StartStream();
        }

        public void ListenTweetFromMe(ReactionRegisterContent obj)
        {
            var user = obj.Owner;

            Authentification(obj);
            var stream = Stream.CreateUserStream();

            stream.TweetCreatedByMe += (sender, args) =>
            {
                SendData(obj, args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
            };
            stream.StartStream();
        }

        public void ListenTweetFromAnyoneButMe(ReactionRegisterContent obj)
        {
            var user = obj.Owner;

            Authentification(obj);
            var stream = Stream.CreateUserStream();

            stream.TweetCreatedByAnyoneButMe += (sender, args) =>
            {
                SendData(obj, args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
            };
            stream.StartStream();
        }

        public void ListenTweetWithKeyWord(ReactionRegisterContent obj)
        {
            string regex = "";
            Authentification(obj);
            var stream = Stream.CreateFilteredStream();

            stream.AddTrack(regex);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                var txt = "A tweet containing '" + regex + "' has been found : " + args.Tweet.FullText;
                SendData(obj, txt);
            };
        }

        public void NewTweet(ServiceActionContent obj)
        {
            Authentification(obj);
            Tweet.PublishTweet(obj.Args.ToString());
        }
    }
}
