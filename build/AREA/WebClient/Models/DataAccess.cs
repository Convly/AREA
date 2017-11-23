using MongoDB.Bson;
using MongoDB.Driver;
using Network.NetTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddAreaToUser(string email, string areaName)
        {
            AreaTree areas = GetAreas(email);
            if (areas != null)
            {
                areas.AreasList.Add(new ATreeRoot(areaName));
                var collection = _db.GetCollection<AreaTree>("AREAs");
                var filter = Builders<AreaTree>.Filter.Eq("Email", email);
                var update = Builders<AreaTree>.Update.Set("AreasList", areas.AreasList);
                collection.UpdateOne(filter, update);
            }
        }

        public void SendTreeToUser(string email, string treeJson)
        {
            List<ATreeRoot> tree = JsonConvert.DeserializeObject<List<ATreeRoot>>(treeJson);

            var collection = _db.GetCollection<AreaTree>("AREAs");
            var filter = Builders<AreaTree>.Filter.Eq("Email", email);
            var update = Builders<AreaTree>.Update.Set("AreasList", tree);
            collection.UpdateOne(filter, update);
        }

        public List<AreaTree> GetAllAreas()
        {
            return _db.GetCollection<AreaTree>("AREAs").Find(new BsonDocument()).ToList();
        }

        public AreaTree GetAreas(string email)
        {
            var filter = Builders<AreaTree>.Filter.Eq("Email", email);
            return _db.GetCollection<AreaTree>("AREAs").Find(filter).FirstOrDefault();
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
                _db.GetCollection<AreaTree>("AREAs").InsertOne(new AreaTree(p.Email));
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