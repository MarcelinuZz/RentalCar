using System;
using Newtonsoft.Json;
using RentalCarClient.Models.Input;
using RentalCarClient.Models.Output;
using RentalCarClient.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentalCarClient.Handler
{
    public class CarHandler : ICarService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public CarHandler(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            _httpClient = new HttpClient(clientHandler);
            _configuration = configuration;
            _baseUrl = _configuration["apiEndpoint"];
        }

        public async Task<ApiResponse<IEnumerable<GetMsCarOutput>>> GetCar(PaginationRequest request)
        {
            var query = $"pageNumber={request.PageNumber}&pageSize={request.PageSize}";

            if (!string.IsNullOrEmpty(request.OrderBy))
                query += $"&orderBy={request.OrderBy}";

            if (request.RentalDate.HasValue && request.ReturnDate.HasValue)
                query += $"&rental_date_request={request.RentalDate.Value:yyyy-MM-dd}&return_date_request={request.ReturnDate.Value:yyyy-MM-dd}";

            if (request.Year.HasValue)
                query += $"&yearRequest={request.Year}";

            string endpoint = $"{_baseUrl}GetCar?{query}";
            var response = await _httpClient.GetAsync(endpoint);

            var apiResponse = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(apiResponse))
            {
                return JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetMsCarOutput>>>(apiResponse);
            }

            return new ApiResponse<IEnumerable<GetMsCarOutput>>
            {
                StatusCode = "500",
                RequestMethod = "GET",
                Data = null
            };
        }
        public async Task<ApiResponse<GetPaymentOutput>> GetPayment(CreatePaymentRequest request)
        {
            string endpoint = $"{_baseUrl}Payment?Car_Id={request.Car_Id}&Rental_Date={request.Rental_Date:yyyy-MM-dd}&Return_Date={request.Return_Date:yyyy-MM-dd}&Name={request.Name}";
            var response = await _httpClient.GetAsync(endpoint);
            var apiResponse = await response.Content.ReadAsStringAsync();
            
            if(!string.IsNullOrEmpty(apiResponse)){
                return JsonConvert.DeserializeObject<ApiResponse<GetPaymentOutput>>(apiResponse);
            }

            return new ApiResponse<GetPaymentOutput>
            {
                StatusCode = "500",
                RequestMethod = "GET",
                Data = null
            };
            
        }

        public async Task<ApiResponse<string>> SubmitRentalPayment(CreateRentalPaymentRequest request)
        {
            string endpoint = $"{_baseUrl}SubmitPayment";
            var response = await _httpClient.PostAsJsonAsync(endpoint, request);
            var apiResponse = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(apiResponse))
            {
                return JsonConvert.DeserializeObject<ApiResponse<string>>(apiResponse);
            }

            return new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                Data = "Error processing payment"
            };
        }
    }
}