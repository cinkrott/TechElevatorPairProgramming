using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.APIClients
{
    public class UserServiceAPI: AuthService
    {
        private const string API_BASE_URL = "https://localhost:44315/User";
        private API_User apiUser = new API_User();

        public List<API_User> GetUsers()
        {
            List<API_User> users = new List<API_User>();
            RestRequest request = new RestRequest(API_BASE_URL);
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }
    }
}
