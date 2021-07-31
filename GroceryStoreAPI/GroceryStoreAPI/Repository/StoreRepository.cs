using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private const string mockDbFilePath = "database.json";

        private ILogger<StoreRepository> logger;

        private Dictionary<int, string> mockDatabase = new Dictionary<int, string>();

        private int currId = int.MinValue;

        private int NewCurrId => Interlocked.Increment(ref currId);

        public StoreRepository(ILogger<StoreRepository> _logger)
        {
            logger = _logger;

            string storeUsersJson = File.ReadAllText(mockDbFilePath);

            List<StoreUser> storeUsers = JsonConvert.DeserializeObject<List<StoreUser>>(storeUsersJson);
            
            foreach(StoreUser storeUser in storeUsers)
            {
                if(!mockDatabase.ContainsKey(storeUser.id))
                {
                    mockDatabase[storeUser.id] = storeUser.name;
                    currId = Math.Max(currId, storeUser.id);
                }
                else
                {
                    logger.LogError("Initial data is not valid, using what's possible.");
                }
            }
            logger.LogDebug("StoreRepository was initialized.");
        }

        public IEnumerable<StoreUser> Get()
        {
            //List<StoreUser> fakeData = new List<StoreUser>()
            //{
            //    new StoreUser() { id=123, name="name123" },
            //    new StoreUser() { id=234, name="name234" },
            //};
            List<StoreUser> allUsers = new List<StoreUser>();
            foreach(int id in mockDatabase.Keys)
            {
                allUsers.Add(new StoreUser() { id = id, name = mockDatabase[id] });
            }

            logger.LogDebug("StoreRepository.Get called");
            return allUsers;
        }

        public string Get(int id)
        {
            logger.LogDebug("StoreRepository.Get called");
            string result = "no-such-user";
            if (mockDatabase.ContainsKey(id))
            {
                result = mockDatabase[id];
            }
            return result;
        }

        public int Post(string name)
        {
            logger.LogDebug("StoreRepository.Post called");
            int newId;
            do
            {
                // hopefully, one loop will be enough...
                newId = NewCurrId;
            } while (!mockDatabase.ContainsKey(newId));
            mockDatabase[newId] = name;
            return newId;
        }

        public bool Put(int id, string name)
        {
            logger.LogDebug("StoreRepository.Put called");
            if(mockDatabase.ContainsKey(id))
            {
                mockDatabase[id] = name;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            logger.LogDebug("StoreRepository.Delete called");
            if (mockDatabase.ContainsKey(id))
            {
                mockDatabase.Remove(id);
                return true;
            }
            return false;
        }
    }
}
