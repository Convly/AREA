using System.Collections.Generic;
using Tweetinvi;
using Service;
using System;

namespace TwitterService
{
    public class TwitterController : Controller
    {
        public TwitterController()
        {
            _reactions = new Dictionary<string, ReactionDelegate>
            {
                { "ListenTweetFromAnyone", ListenTweetFromAnyone },
                { "ListenTweetFromMe", ListenTweetFromMe },
                { "ListenTweetFromAnyoneButMe", ListenTweetFromAnyoneButMe },
                { "ListenTweetWithKeyWord", ListenTweetWithKeyWord}
            };

            _actions = new Dictionary<string, ActionDelegate>
            {
                {"NewTweet", NewTweet }
            };
        }

        public void ListenTweetFromAnyone(Object obj)
        {
            var user = (Network.NetTools.User)obj;
            var stream = Stream.CreateUserStream();
            //object data = null;

            stream.TweetCreatedByAnyone += (sender, args) =>
            {
                //Network.Client.Instance.SendDataToServer(data);
                Console.WriteLine(args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
            };
            stream.StartStream();
        }

        public void ListenTweetFromMe(Object obj)
        {
            var user = (Network.NetTools.User)obj;
            var stream = Stream.CreateUserStream();
            //object data = null;

            stream.TweetCreatedByMe += (sender, args) =>
            {
                //Network.Client.Instance.SendDataToServer(data);
                Console.WriteLine(args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
            };
            stream.StartStream();
        }

        public void ListenTweetFromAnyoneButMe(Object obj)
        {
            var user = (Network.NetTools.User)obj;
            var stream = Stream.CreateUserStream();
            //object data = null;

            stream.TweetCreatedByAnyoneButMe += (sender, args) =>
            {
                //Network.Client.Instance.SendDataToServer(data);
                Console.WriteLine(args.Tweet.CreatedBy.ScreenName + " tweet on your page : " + args.Tweet.FullText);
            };
            stream.StartStream();
        }

        public void ListenTweetWithKeyWord(Object obj)
        {
            string regex = (string)obj;
            var stream = Stream.CreateFilteredStream();
            //object data = null;

            stream.AddTrack(regex);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                //Network.Client.Instance.SendDataToServer(data);
                Console.WriteLine("A tweet containing '" + regex + "' has been found : " + args.Tweet.FullText);
            };
        }

        public void NewTweet(object obj)
        {
            string consumerKeySecret = "SEr6QSvzRD05N9AzIpucxLT3MGUd3wZLnvqk8m7Lq8u26TFbQO";
            string consumerKey = "5xQecQC24p9cTyGPQtiw7O0cE";
            string accessTokenSecret = "";
            string accessToken = "";
            string text = "";

            Auth.SetUserCredentials(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);
            Tweet.PublishTweet(text);
        }
    }
}
