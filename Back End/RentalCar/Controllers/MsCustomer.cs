using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RentalCar.Data;
using RentalCar.Models.Request;
using RentalCar.Models.Result;

namespace RentalCar.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class MsCustomer : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsCustomer(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateLoginRequest request){
            try{

            var NameCustomer = await _context.MsCustomer
            .Where(x => x.email == request.email && x.password == request.password)
            .Select(x => x.name).FirstOrDefaultAsync();

            if (NameCustomer == null){
                return BadRequest(new ApiResponse<string>{
                StatusCode = StatusCodes.Status400BadRequest,
                RequestMethod = HttpContext.Request.Method,
                Data = "Invalid email or password."
                });
            }

            var response = new ApiResponse<String>
            {
                StatusCode = StatusCodes.Status200OK,
                RequestMethod = HttpContext.Request.Method,
                Data = NameCustomer
            };
            
            return Ok(response);
            }catch(Exception ex){
                var response = new ApiResponse<string>{
                    StatusCode = StatusCodes.Status500InternalServerError,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "SOMETHING ERROR"
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            
        }


        [Route("/api/Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateRegisterRequest request)
        {
            try
            {
                var ifEmailExist = await _context.MsCustomer.AnyAsync(x => x.email == request.email);
                if (ifEmailExist)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        RequestMethod = HttpContext.Request.Method,
                        Data = "Email already exists."
                    });
                }

                
                var ifUsernameExist = await _context.MsCustomer.AnyAsync(x => x.name == request.name);
                if (ifUsernameExist)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        RequestMethod = HttpContext.Request.Method,
                        Data = "Username already exists."
                    });
                }

                
                if (request.password != request.RePassword)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        RequestMethod = HttpContext.Request.Method,
                        Data = "Passwords do not match."
                    });
                }


                return Ok(new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status201Created,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Registration successful!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    RequestMethod = HttpContext.Request.Method,
                    Data = "Something went wrong."
                });
            }
        }

        [HttpGet]
        [Route("/api/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("LoginPage", "Auth");
        }

    }
}
