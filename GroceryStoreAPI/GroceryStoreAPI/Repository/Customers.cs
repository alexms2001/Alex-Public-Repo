using System.Collections.Generic;

namespace GroceryStoreAPI.Repository
{
    /// <summary>
    /// To match initial database.json format
    /// </summary>
    public class Customers
    {
        public List<StoreUser> customers { get; set; }
    }
}
