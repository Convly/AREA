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
        /// Constructor for <see cref="User"/>
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        /// <param name="pwd">The <see cref="User"/>'s password</param>
        public User(string email, string pwd)
        {
            Email = email;
            Pwd = pwd;
            LastUpdated = null;
        }

        /// <summary>
        /// Check if <see cref="User"/>'s tokens are expired
        /// </summary>
        /// <returns>A <see cref="bool"/> if tokens are expired or not</returns>
        public bool AreTokensExpired()
        {
            return (LastUpdated == null || DateTime.UtcNow.Subtract((DateTime)LastUpdated).TotalMinutes >= 120.0f);
        }

        /// <summary>
        /// The <see cref="ObjectId"/> <see cref="User"/>'s identifier
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The <see cref="User"/>'s email
        /// </summary>
        [BsonElement("Email")]
        public string Email { get; set; }

        /// <summary>
        /// The <see cref="User"/>'s password
        /// </summary>
        [BsonElement("Pwd")]
        public string Pwd { get; set; }

        private Dictionary<string, string> accessToken;

        /// <summary>
        /// The <see cref="User"/>'s standard tokens
        /// </summary>
        public Dictionary<string, string> AccessToken { get => accessToken; set => accessToken = value; }

        private Dictionary<string, string> accessTokenSecret;

        /// <summary>
        /// The <see cref="User"/>'s secret tokens
        /// </summary>
        public Dictionary<string, string> AccessTokenSecret { get => accessTokenSecret; set => accessTokenSecret = value; }

        /// <summary>
        /// The last updated <see cref="DateTime"/> for the <see cref="User"/>'s tokens
        /// </summary>
        public Nullable<DateTime> LastUpdated { get; set; }
    }
}