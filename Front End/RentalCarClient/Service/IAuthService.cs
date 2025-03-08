using System;
using RentalCarClient.Models.Input;
using RentalCarClient.Models.Output;

namespace RentalCarClient.Service
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Login(LoginRequest request);
        Task<ApiResponse<string>> Register(RegisterRequest request);
    }
}