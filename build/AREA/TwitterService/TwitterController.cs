using System.Collections.Generic;
using Tweetinvi;
using Service;
using System;
using Network.Events;
using Network.NetTools;
using static Network.Events.AddUserEvent;

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

        public void Reaction(Event obj, object reactionContent)
        {
            Network.NetTools.User user = obj.OwnerInfos;
            Event react = new TriggerReactionEvent(HttpEventSource.SERVICE, HttpEventType.COMMAND, user, reactionContent);
            Packet packet = new Packet(_name, PacketCommand.REACTION, react);

            Network.Client.Instance.SendDataToServer(packet);
            Console.WriteLine(reactionContent.ToString());
        }

        public void ListenTweetFromAnyone(Event obj)
        {
            var user = obj.OwnerInfos;
            var stream = Stream.CreateUserStream();

            Authentification(obj);
            stream.TweetCreatedByAnyone += (sender, args) =>
            {
                Reaction(obj, new { resume = args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText });
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
                Reaction(obj, new { resume = args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText });
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
                Reaction(obj, new { resume = args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText });
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
                Reaction(obj, new { resume = "A tweet containing '" + regex + "' has been found : " + args.Tweet.FullText });
            };
        }

        public void NewTweet(Event obj)
        {
            Authentification(obj);
            string text = (string)obj.Data;
            Tweet.PublishTweet(text);
        }
    }
}
