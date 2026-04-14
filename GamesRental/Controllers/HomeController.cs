namespace GamesRental.Web.Controllers
{
    using System.Diagnostics;
    using GamesRental.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/StatusCode")]
        public IActionResult StatusCode(int code)
        {
            return code switch
            {
                404 => View("Error404"),
                500 => View("Error500"),
                _ => View("Error")
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Response.StatusCode = 500;

            return View("Error500", new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
