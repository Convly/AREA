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
            var users = GetUsers();
            foreach (var user in users)
            {
                try
                {
                    Dispatcher.AddUser(user);
                    var trees = GetAreas(user.Email).AreasList;
                    foreach (var tree in trees)
                    {
                        Dispatcher.AddTree(user, tree);
                    }
                }
                catch { }
            }

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

        public void AddTokensAccess(User user)
        {
            Dispatcher.AddTokensAccess(user);
        }

        public void SendTreeToUser(string email, string treeJson, int treeIndex)
        {
            List<ATreeRoot> trees = JsonConvert.DeserializeObject<List<ATreeRoot>>(treeJson);

            var collection = _db.GetCollection<AreaTree>("AREAs");
            var filter = Builders<AreaTree>.Filter.Eq("Email", email);
            var update = Builders<AreaTree>.Update.Set("AreasList", trees);
            collection.UpdateOne(filter, update);

            User userTree;
            ATreeRoot tree;
            try
            {
                userTree = GetUser(email);
                tree = trees[treeIndex];
            }
            catch
            {
                return;
            }
            Dispatcher.AddTree(userTree, tree);
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

        public bool Create(User user)
        {
            try
            {
                _db.GetCollection<User>("Authentification").InsertOne(user);
                _db.GetCollection<AreaTree>("AREAs").InsertOne(new AreaTree(user.Email));
                Dispatcher.AddUser(user);
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