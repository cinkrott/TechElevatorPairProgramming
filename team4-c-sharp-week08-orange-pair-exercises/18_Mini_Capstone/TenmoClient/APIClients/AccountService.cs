using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.APIClients
{
    public class AccountService: AuthService
    {
        private const string API_BASE_URL = "https://localhost:44315/account/";
        private API_User apiUser = new API_User();
        
        
        
        public decimal GetBalance()
        {
            
            RestRequest request = new RestRequest(API_BASE_URL);
            IRestResponse<decimal> response = client.Get<decimal>(request);
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

        public bool Transfer(DataRequest transfer)
        {
            RestRequest request = new RestRequest(API_BASE_URL);
            request.AddJsonBody(transfer);
            IRestResponse<bool> response = client.Post<bool>(request);
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
