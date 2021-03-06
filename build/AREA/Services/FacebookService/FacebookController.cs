﻿using Network.Events;
using Network.NetTools;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FacebookService
{
    /// <summary>
    /// This class allow the implementation of a service with the twitter API.
    /// </summary>
    public class FacebookController : Controller
    {
        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="name">The name of the API.</param>
        public FacebookController(string name)
        {
            _name = name;
            _reactions = new Dictionary<string, ReactionDelegate>
            {
            };
            _actions = new Dictionary<string, ActionDelegate>
            {
                { "Post", Post }
            };
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public void Post(ServiceActionContent obj)
        {
            var accessToken = obj.User.AccessToken[_name];
            string message = obj.Args as string;

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://graph.facebook.com/v2.10/")
            };
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = GetPayload(new { message });

            httpClient.PostAsync($"me/feed?access_token={accessToken}", payload).Wait();
        }
    }
}