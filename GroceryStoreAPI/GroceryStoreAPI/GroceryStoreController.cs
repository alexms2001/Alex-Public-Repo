using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI
{
    [Route("[controller]")]
    [ApiController]
    public class GroceryStoreController : ControllerBase
    {
        protected ILogger logger;

        public GroceryStoreController(ILogger<GroceryStoreController> _logger)
        {
            logger = _logger;
        }

        // GET: /GroceryStore
        [HttpGet]
        public IEnumerable<string> Get()
        {
            logger.LogDebug("Getting all");
            return new string[] { "value1", "value2" };
        }

        // GET /GroceryStore/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            logger.LogDebug("Getting id={id}", id);
            return "value";
        }

        // POST /GroceryStore
        [HttpPost]
        public void Post([FromBody] string value)
        {
            logger.LogDebug("Posting value={value}", value);
            int qqq = 0;
            ++qqq;          // for a breakpoint, avoiding "unused vaiable" warning
        }

        // PUT /GroceryStore/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            logger.LogDebug("Putting id={id}, value={value}", id, value);
            int qqq = 0;
            ++qqq;          // for a breakpoint, avoiding "unused vaiable" warning
        }

        // DELETE /GroceryStore/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logger.LogDebug("Deleting id={id}", id);
        }
    }
}
