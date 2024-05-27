using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TestRest.Utilities;

namespace TestRest
{
    public class Tests
    {
        private HttpHelper _helper;

        [SetUp]
        public void Setup()
        {
            _helper = new HttpHelper();
        }

        [Test]
        public void GetAllUsers()
        {
            var responseFromHelper = _helper.Get("users");
        }

        [Test]
        public async Task CreateUser()
        {
            var requestBody = _helper.CreateUserBody(Guid.NewGuid().ToString(), "female", "active");
            var response = await _helper.Post("users", requestBody);

            Assert.AreEqual("Created", response.StatusCode.ToString());
        }

        [Test]
        public async Task GetSpecificUser()
        {
            //POST - to create a new user
            var requestBody = _helper.CreateUserBody(Guid.NewGuid().ToString(), "female", "active");
            var response = await _helper.Post("users", requestBody);
            var result = response.Content.ReadAsStringAsync().Result;
   
        
            //GET - users/{user.ID}
            var user = await _helper.Get("users");

            


            //Assert the response
        }
    }
}