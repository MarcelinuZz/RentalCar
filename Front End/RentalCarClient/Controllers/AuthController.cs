using Microsoft.AspNetCore.Mvc;
using RentalCarClient.Models.Input;
using RentalCarClient.Service;

namespace RentalCarClient.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {   
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("Login")]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.Login(request);
            if (result.StatusCode == "200")
            {
                HttpContext.Session.SetString("UserName", result.Data);
                return RedirectToAction("Index", "Home");
            }
            else
            {   
                ViewBag.ErrorMessage = result.Data;
                return View("LoginPage");
            }
        }

        [HttpGet("LogOut")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }

        [HttpGet("RegisterPage")]
        public IActionResult RegisterPage()
        {
            return View();
        }

       [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);
            
            if (result.StatusCode == "400") 
            {
                ViewBag.ErrorMessage = result.Data; 
                return View("RegisterPage");
            }

            return RedirectToAction("LoginPage"); 
        }
            }
}
