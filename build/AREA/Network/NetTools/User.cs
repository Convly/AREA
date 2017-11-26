using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Network.NetTools
{
    /// <summary>
    /// User information
    /// </summary>
    public class User
    {
        public User(string email, string pwd)
        {
            Email = email;
            Pwd = pwd;
            LastUpdated = null;
        }

        public bool AreTokensExpired()
        {
            return (LastUpdated == null || DateTime.UtcNow.Subtract((DateTime)LastUpdated).TotalMinutes >= 120.0f);
        }

        public ObjectId Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Pwd")]
        public string Pwd { get; set; }

        private Dictionary<string, string> accessToken;
        public Dictionary<string, string> AccessToken { get => accessToken; set => accessToken = value; }

        private Dictionary<string, string> accessTokenSecret;
        public Dictionary<string, string> AccessTokenSecret { get => accessTokenSecret; set => accessTokenSecret = value; }

        public Nullable<DateTime> LastUpdated { get; set; }
    }
}