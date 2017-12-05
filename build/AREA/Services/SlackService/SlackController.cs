using System.Collections.Specialized;
using System.Collections.Generic;
using Network.NetTools;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using Service;
using System;

public class Payload
{
    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}

namespace SlackService
{
    public class SlackController : Controller
    {
        private readonly Encoding _encoding = new UTF8Encoding();

        public SlackController(string name)
        {
            _name = name;
            _reactions = new Dictionary<string, ReactionDelegate>
            {
            };
            _actions = new Dictionary<string, ActionDelegate>
            {
                { "PostMessage", PostMessage }
            };
        }

        public void PostMessage(ServiceActionContent obj)
        {
            string text = obj.Args as string;
            string username = "NexusArea";
            string channel = null;
            Uri _uri = new Uri(obj.User.AccessToken[_name]);

            Payload payload = new Payload()
            {
                Channel = channel,
                Username = username,
                Text = text
            };

            string payloadJson = JsonConvert.SerializeObject(payload);

            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response = client.UploadValues(_uri, "POST", data);

                string responseText = _encoding.GetString(response);
            }
        }

    }
}
