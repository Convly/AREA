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
        /// <summary>
        /// Consructor of User
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="pwd">password</param>
        public User(string email, string pwd)
        {
            Email = email;
            Pwd = pwd;
            LastUpdated = null;
        }

        /// <summary>
        /// return the state of the token
        /// </summary>
        /// <returns></returns>
        public bool AreTokensExpired()
        {
            return (LastUpdated == null || DateTime.UtcNow.Subtract((DateTime)LastUpdated).TotalMinutes >= 120.0f);
        }

        /// <summary>
        /// User's Id
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        [BsonElement("Email")]
        public string Email { get; set; }

        /// <summary>
        /// User's pwd
        /// </summary>
        [BsonElement("Pwd")]
        public string Pwd { get; set; }

        private Dictionary<string, string> accessToken;
        /// <summary>
        /// Get the token access
        /// </summary>
        public Dictionary<string, string> AccessToken { get => accessToken; set => accessToken = value; }

        private Dictionary<string, string> accessTokenSecret;

        /// <summary>
        /// Get the secret token access
        /// </summary>
        public Dictionary<string, string> AccessTokenSecret { get => accessTokenSecret; set => accessTokenSecret = value; }

        /// <summary>
        /// Get the last update of the token
        /// </summary>
        public Nullable<DateTime> LastUpdated { get; set; }
    }
}