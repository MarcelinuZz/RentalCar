using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models.Request;
using RentalCar.Models.Result;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.Xml;
using RentalCar.Models;

namespace RentalCar.Controllers
{
    [Route("api/GetCar")]
    [ApiController]
    public class MsCar : ControllerBase
    {
        private readonly AppDbContext _context;
        public MsCar(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMsCarResult>>> index(int pageNumber, int pageSize, string? orderBy, DateTime? rental_date_request, DateTime? return_date_request, int? yearRequest)
        {
            try{

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            
            if (pageSize < 1)
            {
                pageSize = 6; 
            }

            var skip = (pageNumber - 1) * pageSize;

            var query  = await _context.MsCar
            .Include(x => x.Rentals)
            .ToListAsync();

            if(yearRequest != null){
                query = query.Where(x => x.year == yearRequest && x.status == true).ToList();
            }

             if (rental_date_request != null && return_date_request != null)
                {
                    if(rental_date_request <= return_date_request){
                        query = query.Where(x => !_context.TrRental
                        .Any(r => r.car_id == x.Car_id &&
                        r.rental_date <= return_date_request && 
                        r.return_date >= rental_date_request)).ToList();
                    } else if(rental_date_request > return_date_request){
                        throw new ArgumentException ("ReturnDate Cannot be Lower than RentalDate");
                    }
                }
            var listCar = query
                .Select(x => new 
                {
                    carId = x.Car_id,
                    name = $"{x.name} {x.year}",
                    number_of_car_seats = x.number_of_car_seats,
                    transmission = x.transmission,
                    price_per_day = x.price_per_day,
                    status = x.status
                })
                .ToList();

            var listCarId = listCar.Select(x => x.carId).ToList();

            var listCarImage = await _context.MsCarImages
                .Where(x => listCarId.Any(y => y == x.Car_id))
                .ToListAsync();

            var joinedResult = listCar.GroupJoin(
                listCarImage,
                car => car.carId,
                image => image.Car_id,
                (car, images) => new GetMsCarResult
                {   
                    CarId = car.carId,
                    name = car.name,
                    number_of_car_seats = car.number_of_car_seats,
                    transmission = car.transmission,
                    price_per_day = car.price_per_day,
                    status = car.status,
                    image_link = images.Select(img => img.image_link).ToList()
                });

            if (orderBy == "ASC")
            {
                joinedResult = joinedResult.OrderBy(x => x.price_per_day); 
            }
            
            if(orderBy == "DESC")
            {
                joinedResult = joinedResult.OrderByDescending(x => x.price_per_day); 
            }

            var paginatedResult = joinedResult
                .Skip(skip)
                .Take(pageSize) 
                .ToList();

            

            var response = new ApiResponse<IEnumerable<GetMsCarResult>>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = paginatedResult
            };

            return Ok(response);
        

        } catch(ArgumentException ex){
            return BadRequest(new { Message = ex.Message });
        }

        catch(Exception ex){
            return StatusCode(500, ex.Message);
        }


      }

      [Route("/api/payment")]
      [HttpGet]
      public async Task<ActionResult<GetPaymentResult>> GetPayment([FromQuery] CreatePaymentRequest request){
            var SelectedCar = await _context.MsCar
            .Where(c => c.Car_id == request.Car_Id)
            .FirstOrDefaultAsync();

            if(SelectedCar == null){
                return NotFound("Car not found" );
            }

            var TotalDays = (request.Return_Date - request.Rental_Date).Days;
            if(TotalDays < 1){
                TotalDays = 1;
            }
            var TotalPrices = TotalDays * SelectedCar.price_per_day;

            var IMGcar = await _context.MsCarImages
            .Where(x => x.Car_id == request.Car_Id)
            .FirstOrDefaultAsync();

            var getEmail = await _context.MsCustomer
            .Where(x => x.name == request.Name)
            .Select(x => x.email)
            .FirstOrDefaultAsync();

            var result = new GetPaymentResult{
                Car_name = $"{SelectedCar.name} {SelectedCar.year}",
                transmission = SelectedCar.transmission,
                Number_Seat = SelectedCar.number_of_car_seats,
                Name_Customer = $"{request.Name} ({getEmail})",
                RentalDate = $"{request.Rental_Date} sampai {request.Return_Date}",
                Price_Per_Day = SelectedCar.price_per_day ?? 0,
                TotalPrice = TotalPrices ?? 0,
                ImageLink = IMGcar.image_link
            };

            var response = new ApiResponse<GetPaymentResult>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = result
            };

            return Ok(response);
      }


      [Route("/api/SubmitPayment")]
      [HttpPost]

      public async Task<IActionResult> PostPayment([FromBody] CreateRentalPaymentRequest request){
        try{

            var splitName = request.UserName.Split('(')[0].Trim();
            var carNameOnly = request.CarName.Substring(0, request.CarName.LastIndexOf(' ')).Trim(); 

            
            var getCarId = await _context.MsCar
            .Where(x => x.name == carNameOnly)
            .FirstOrDefaultAsync();

            var getUserId = await _context.MsCustomer
            .Where(x => x.name == splitName)
            .FirstOrDefaultAsync();

            var GetTopId = await _context.TrRental
            .OrderByDescending(x => x.Rental_id)
            .Select(x => x.Rental_id)
            .FirstOrDefaultAsync();

            GetTopId += 1;

            var newData = new TrRental(){
                Rental_id = GetTopId,
                car_id = getCarId.Car_id,
                customer_id = getUserId.Customer_id,
                rental_date = request.RentalDate,
                return_date = request.ReturnDate,
                total_price = request.TotalPrice,
                payment_status = true
            };

            _context.TrRental.Add(newData);
            await _context.SaveChangesAsync();

            var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status201Created,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Rental data saved successfully"
                };

            return Ok(response);
        }
        catch(Exception ex){
            var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status400BadRequest,
                    RequestMethod = HttpContext.Request.Method,
                    Data = ex.Message
                };
            return BadRequest(response);
        }
      }
    }
}