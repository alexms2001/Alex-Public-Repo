using System.ComponentModel.DataAnnotations;

namespace GroceryStoreAPI.Repository
{
    public class StoreUser
    {
        // I will not use this attribute in a mock database, but let it be. Does not hurt, anyway.
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
