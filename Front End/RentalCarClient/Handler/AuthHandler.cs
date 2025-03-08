using System;
using Newtonsoft.Json;
using RentalCarClient.Models.Input;
using RentalCarClient.Models.Output;
using RentalCarClient.Service;

namespace RentalCarClient.Handler
{
    public class AuthHandler : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public AuthHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["apiEndpoint"];

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _httpClient = new HttpClient(clientHandler);

        }

        public async Task<ApiResponse<string>> Login(LoginRequest request)
        {
            string endpoint = _baseUrl + "Login"; 
            var response = await _httpClient.PostAsJsonAsync(endpoint, request);

            if (!response.IsSuccessStatusCode){
            var errorResponse = await response.Content.ReadAsStringAsync();
            return new ApiResponse<string>{
                StatusCode = response.StatusCode.ToString(),
                RequestMethod = "POST",
                Data = "Invalid login credentials" 
                };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(apiResponse))
            {
                try 
                {
                    return JsonConvert.DeserializeObject<ApiResponse<string>>(apiResponse);
                }
                catch (Exception ex)
                {
                    return new ApiResponse<string>
                    {
                        StatusCode = "500",
                        RequestMethod = "POST",
                        Data = "An error occurred while processing the login response."
                    };
                }
            }

            return new ApiResponse<string>
            {
                StatusCode = "500",
                RequestMethod = "POST",
                Data = "Empty response from server."
            };
        }


    public async Task<ApiResponse<string>> Register(RegisterRequest request)
    {
        string endpoint = _baseUrl + "Register";
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);

        var apiResponse = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrEmpty(apiResponse)) 
        {
            return JsonConvert.DeserializeObject<ApiResponse<string>>(apiResponse);
        }

        return new ApiResponse<string>
        {
             StatusCode = "500",
            RequestMethod = "POST",
            Data = "Error during registration"
        };
    }
    }
}