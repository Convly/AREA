using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Web;

namespace WebClient.Models
{
    public class DataAccess
    {
        private static DataAccess instance = null;
        public static DataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataAccess();
                }
                return instance;
            }
        }

        MongoClient     _client;
        IMongoDatabase  _db;

        private DataAccess()
        {
            _client = new MongoClient();
            _db = _client.GetDatabase("Area");
        }

        ~DataAccess()
        {
            _client.DropDatabase("Area");
        } 

        public List<User> GetUsers()
        {
            return _db.GetCollection<User>("Authentification").Find(new BsonDocument()).ToList();
        }


        public User GetUser(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            return _db.GetCollection<User>("Authentification").Find(filter).FirstOrDefault();
        }

        public bool Create(User p)
        {
            try
            {
                _db.GetCollection<User>("Authentification").InsertOne(p);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public void Update(string email, User p)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            var update = Builders<User>.Update.Set("Email", p.Email);
            update.Set("Pwd", p.Pwd);

            _db.GetCollection<User>("Authentification").UpdateOne(filter, update);
        }

        public void Remove(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            _db.GetCollection<User>("Authentification").DeleteOne(filter);
        }
    }
}