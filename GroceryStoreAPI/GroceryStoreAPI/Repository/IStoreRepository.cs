using System.Collections.Generic;

namespace GroceryStoreAPI.Repository
{
    public interface IStoreRepository
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Enumeration of all users</returns>
        IEnumerable<StoreUser> Get();
        
        /// <summary>
        /// Get user name by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User name.</returns>
        string Get(int id);
        
        /// <summary>
        /// Create a user. Names can be duplicate.
        /// </summary>
        /// <param name="name">User name.</param>
        /// <returns>An id for newly created user or -1 on failure.</returns>
        int Post(string name);
        
        /// <summary>
        /// Update user name by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="name">New user name.</param>
        /// <returns>True if a user was updated successfully, false otherwise (does not exist, maybe).</returns>
        bool Put(int id, string name);

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>True on success, false on failure (an id does not exist, maybe).</returns>
        bool Delete(int id);
    }
}
