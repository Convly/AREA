using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace WebClient.Models
{
    public class User
    {
        public User(string email, string pwd)
        {
            Email = email;
            Pwd = pwd;
            AreasList = new List<Area>();
        }

        public ObjectId Id { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Pwd")]
        public string Pwd { get; set; }
        [BsonElement("AreasList")]
        public List<Area> AreasList { get; set; }
    }
}