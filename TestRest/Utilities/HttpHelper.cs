using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRest.Models;

namespace TestRest.Utilities
{
    public class HttpHelper
    {
        private readonly HttpClient _httpClient;

        public HttpHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://gorest.co.in/public/v2/");
            _httpClient.DefaultRequestHeaders
                .Add("Authorization", "Bearer ff0cff9050bfee5de9386da126e3f76128cfe323d049505c46612fa55f8c8a66");
        }

        public async Task<HttpResponseMessage> Get(string endpoint)
        {
            return await _httpClient.GetAsync(endpoint);
        }

        public async Task<HttpResponseMessage> Post(string endpoint, StringContent body)
        {
            return await _httpClient.PostAsync(endpoint, body);
        }

        public async Task<UserResponse> CreateNewUser(string endpoint, string name, string gender, string status)
        {
            var userBody = CreateUserBody(name, gender, status);
            var responseMessage = await _httpClient.PostAsync(endpoint, userBody);
            var jsonContent = responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserResponse>(jsonContent.Result);
        }

        public StringContent CreateUserBody(string name, string gender, string status)
        {
            var user = new UserRequest
            {
                Name = name,
                Email = Email(name),
                Gender = gender,
                Status = status
            };

            var userBody = JsonConvert.SerializeObject(user);

            return new StringContent(userBody, Encoding.UTF8, "application/json");

        }

        public string Email(string name)
        {
            return $"{name}@domain.com";
        }
    }
}
