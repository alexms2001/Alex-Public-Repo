using GroceryStoreAPI.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GSAT
{
    [TestClass]
    public class GroceryStoreAPITests
    {
        /// <summary>
        /// In a serious case I'd involve Moq (https://github.com/Moq/moq4/wiki/Quickstart), of course.
        /// Keeping things as simple as possible...
        /// </summary>

        private IStoreRepository storeRepo = new StoreRepository(null);

        [TestMethod]
        public void A_TestRepoGet()
        {
            string allUsersJsonExpected = "[{\"id\":1,\"name\":\"Bob\"},{\"id\":2,\"name\":\"Mary\"},{\"id\":3,\"name\":\"Joe\"}]";
            List<StoreUser> allUsers = storeRepo.Get().ToList();
            string allUsersJson = JsonConvert.SerializeObject(allUsers);
            Assert.AreEqual(allUsersJson, allUsersJsonExpected);
        }

        [TestMethod]
        public void B_TestRepoGetById()
        {
            string expectedName = "Mary";
            string name = storeRepo.Get(2);
            Assert.AreEqual(name, expectedName);
        }

        [TestMethod]
        public void C_TestRepoPost()
        {
            int expectedId = 4;
            string name = "Alex";
            int id = storeRepo.Post(name);
            Assert.AreEqual(id, expectedId);
        }

        [TestMethod]
        public void D_TestRepoPut()
        {
            int id = 4;
            string newName = "AlexUpdated";
            bool isSuccess = storeRepo.Put(id, newName);
            Assert.IsTrue(isSuccess);
            string name = storeRepo.Get(id);
            Assert.AreEqual(name, newName);
        }

        [TestMethod]
        public void E_TestRepoDelete()
        {
            int id = 4;
            bool isSuccess = storeRepo.Delete(id);
            Assert.IsTrue(isSuccess);
            A_TestRepoGet();              // verify that we went back to initial state
        }
    }
}
