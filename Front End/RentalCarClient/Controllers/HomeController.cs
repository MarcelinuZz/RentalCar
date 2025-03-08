using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentalCarClient.Models;
using RentalCarClient.Handler;
using RentalCarClient.Models.Input;
using System.Threading.Tasks;
using RentalCarClient.Service;

namespace RentalCarClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarService _carService;

    public HomeController(ILogger<HomeController> logger, ICarService carService)
    {
        _logger = logger;
        _carService = carService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        
        var request = new PaginationRequest { PageNumber = pageNumber, PageSize = 6 };
        var cars = await _carService.GetCar(request);
        ViewBag.CurrentPage = pageNumber; 
        return View("Index", cars.Data); 
    }

    [HttpGet("GetCar")]
    public async Task<IActionResult> GetCar(PaginationRequest request)
    {
        var cars = await _carService.GetCar(request);
        ViewBag.CurrentPage = request.PageNumber; 
        return View("Index",cars.Data); 
    }

    [HttpGet("PaymentDetail")]
    public async Task<IActionResult> PaymentDetail(CreatePaymentRequest request)
    {
        var paymentDetail = await _carService.GetPayment(request);
        return View("PaymentDetail", paymentDetail.Data); 
    }

    [HttpPost("SubmitRentalPayment")]
    public async Task<IActionResult> SubmitRentalPayment(CreateRentalPaymentRequest request)
    {
        var result = await _carService.SubmitRentalPayment(request);

        if (result.StatusCode == StatusCodes.Status201Created.ToString())
        {
            return View("Index");
        }

        ViewBag.ErrorMessage = "Gagal memproses pembayaran. Silakan coba lagi.";
        return View("PaymentDetail", request);
    }

}


