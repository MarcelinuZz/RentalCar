using System;
using RentalCarClient.Models.Input;
using RentalCarClient.Models.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCarClient.Service
{
    public interface ICarService
    {
        Task<ApiResponse<IEnumerable<GetMsCarOutput>>> GetCar(PaginationRequest request);
        Task<ApiResponse<GetPaymentOutput>> GetPayment(CreatePaymentRequest request);
        Task<ApiResponse<string>> SubmitRentalPayment(CreateRentalPaymentRequest request);
    }
}
