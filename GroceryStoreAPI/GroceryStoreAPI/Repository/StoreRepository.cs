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

        /// <summary>
        /// Atomic operation, thread-safe:
        /// </summary>
        private int NewCurrId => Interlocked.Increment(ref currId);

        private object lockForPost = new object();

        /// <summary>
        /// Does not allow me to make it async so File.ReadAllTextAsync etc. cannot be used.
        /// Not that it matters much - whatever is done just _once_, _can_ by sync. No harm done.
        /// </summary>
        /// <param name="_logger">Implementation of scoped ILogger.</param>
        public StoreRepository(ILogger<StoreRepository> _logger)
        {
            logger = _logger;

            string storeUsersJson = File.ReadAllText(mockDbFilePath);

            Customers customers = JsonConvert.DeserializeObject<Customers>(storeUsersJson);
            
            foreach(StoreUser storeUser in customers.customers)
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

            string jsonForDict = JsonConvert.SerializeObject(mockDatabase);

            logger.LogDebug("StoreRepository was initialized.");
        }

        public IEnumerable<StoreUser> Get()
        {
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

        /// <summary>
        /// Have to go from dictionary to list and back just b/c of initial format of mock database.json
        /// so that new runs can start from both initial and modified formats.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        private void SaveUpdatedDb()
        {
            lock (lockForPost)
            {
                List<StoreUser> storeUsers = new List<StoreUser>();
                foreach (int id_ in mockDatabase.Keys)
                {
                    storeUsers.Add(new StoreUser() { id = id_, name = mockDatabase[id_] });
                }
                Customers customers = new Customers() { customers = storeUsers };
                string customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
                File.WriteAllText(mockDbFilePath, customersJson);
            }
        }

        /// <summary>
        /// Should be async with File.WriteAllTextAsync, but I have to lock, and
        /// await is not available inside a critical section aka lock...
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int Post(string name)
        {
            logger.LogDebug("StoreRepository.Post called");
            int newId;
            do
            {
                // hopefully, one loop will be enough...
                newId = NewCurrId;
            } while (mockDatabase.ContainsKey(newId));
            mockDatabase[newId] = name;
            SaveUpdatedDb();

            return newId;
        }

        /// <summary>
        /// Same about not being async...
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Put(int id, string name)
        {
            logger.LogDebug("StoreRepository.Put called");
            if(mockDatabase.ContainsKey(id))
            {
                mockDatabase[id] = name;
                SaveUpdatedDb();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Same about not being async...
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            logger.LogDebug("StoreRepository.Delete called");
            if (mockDatabase.ContainsKey(id))
            {
                string name = mockDatabase[id];
                mockDatabase.Remove(id);
                SaveUpdatedDb();
                return true;
            }
            return false;
        }
    }
}
