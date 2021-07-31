using GroceryStoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI
{
    [Route("[controller]")]
    [ApiController]
    public class GroceryStoreController : ControllerBase
    {
        protected ILogger logger;

        protected IStoreRepository storeRepo;

        public GroceryStoreController(ILogger<GroceryStoreController> _logger, IStoreRepository _storeRepo)
        {
            logger = _logger;
            storeRepo = _storeRepo;
            logger.LogDebug("GroceryStoreController was initialized.");
        }

        // GET: /GroceryStore
        /// <summary>
        /// Gets all users with their IDs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StoreUser> Get()
        {
            logger.LogDebug("Getting all users");
            return storeRepo.Get();
        }

        // GET /GroceryStore/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            logger.LogDebug("Getting id={id}", id);
            return storeRepo.Get(id);
        }

        // POST /GroceryStore
        [HttpPost]
        public int Post([FromBody] string name)
        {
            logger.LogDebug("Posting name={name}", name);
            int newId = storeRepo.Post(name);
            logger.LogDebug("Posting name={name} returns id={id}", name, newId);
            return newId;
        }

        // PUT /GroceryStore/5
        /// <summary>
        /// Return true on success, false on failure
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="name">User name</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] string name)
        {
            logger.LogDebug("Putting id={id}, value={name}", id, name);
            return storeRepo.Put(id, name);
        }

        // DELETE /GroceryStore/5
        /// <summary>
        /// Return true on success, false on failure
        /// </summary>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            logger.LogDebug("Deleting id={id}", id);
            return storeRepo.Delete(id);
        }
    }
}
