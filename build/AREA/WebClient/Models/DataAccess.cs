using MongoDB.Bson;
using MongoDB.Driver;
using Network.NetTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebClient.Models
{
    /// <summary>
    /// Singleton that interacts with a database MongoDB
    /// </summary>
    public class DataAccess
    {
        private static DataAccess instance = null;

        /// <summary>
        /// The instance of the <see cref="DataAccess"/> singleton
        /// </summary>
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

        private MongoClient     _client;
        private IMongoDatabase  _db;

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

        /// <summary>
        /// Add an new AREA to a specific user
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        /// <param name="areaName">The AREA's name</param>
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

        /// <summary>
        /// Send to the <see cref="Dispatcher"/> the <see cref="User"/>'s token
        /// </summary>
        /// <param name="user">An <see cref="User"/></param>
        public void AddTokensAccess(User user)
        {
            Dispatcher.AddTokensAccess(user);
        }

        /// <summary>
        /// Send to the database all the trees for an specific user
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        /// <param name="treeJson">The entire tree data of the AREA serialized</param>
        /// <param name="treeIndex">The tree index to send to the database</param>
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

        /// <summary>
        /// Update on the database <see cref="User"/>'s tokens
        /// </summary>
        /// <param name="user">An <see cref="User"/></param>
        public void UpdateUserToken(User user)
        {
            var collection = _db.GetCollection<AreaTree>("Authentification");
            var filter = Builders<AreaTree>.Filter.Eq("Email", user.Email);
            var update = Builders<AreaTree>.Update.Set("AccessToken", user.AccessToken)
                                                  .Set("AccessTokenSecret", user.AccessTokenSecret)
                                                  .Set("LastUpdated", DateTime.Now);
            collection.UpdateOne(filter, update);
        }

        /// <summary>
        /// Get all AREAs
        /// </summary>
        /// <returns>A list of <see cref="AreaTree"/></returns>
        public List<AreaTree> GetAllAreas()
        {
            return _db.GetCollection<AreaTree>("AREAs").Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Get all AREAs for a specific user
        /// </summary>
        /// <param name="email">The <see cref="User"/> email</param>
        /// <returns>An <see cref="AreaTree"/> for the specific <see cref="User"/></returns>
        public AreaTree GetAreas(string email)
        {
            var filter = Builders<AreaTree>.Filter.Eq("Email", email);
            return _db.GetCollection<AreaTree>("AREAs").Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>A list of <see cref="User"/></returns>
        public List<User> GetUsers()
        {
            return _db.GetCollection<User>("Authentification").Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// Get an specific user
        /// </summary>
        /// <param name="email">The <see cref="User"/> email</param>
        /// <returns>The specific <see cref="User"/></returns>
        public User GetUser(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            return _db.GetCollection<User>("Authentification").Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The <see cref="User"/> to add in the database</param>
        /// <returns>Returns if an <see cref="User"/> has been created or not</returns>
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

        /// <summary>
        /// Update an user on the database by its email with an new <see cref="User"/> class
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        /// <param name="user">The new <see cref="User"/> instance</param>
        public void Update(string email, User user)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            var update = Builders<User>.Update.Set("Email", user.Email);
            update.Set("Pwd", user.Pwd);

            _db.GetCollection<User>("Authentification").UpdateOne(filter, update);
        }

        /// <summary>
        /// Remove an user on the database by its email
        /// </summary>
        /// <param name="email">The <see cref="User"/>'s email</param>
        public void Remove(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);
            _db.GetCollection<User>("Authentification").DeleteOne(filter);
        }
    }
}