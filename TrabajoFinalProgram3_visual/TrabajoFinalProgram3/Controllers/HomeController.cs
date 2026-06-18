using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinalProgram3.Models;
using TrabajoFinalProgram3.Services;
using System.Threading.Tasks;

namespace TrabajoFinalProgram3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WalletService _walletService;
        public HomeController(
        ILogger<HomeController> logger,
        WalletService walletService)
        {
            _logger = logger;
            _walletService = walletService;
        }

        public async Task<IActionResult> Index()
        {
            decimal patrimonio = await _walletService.ObtenerPatrimonioAsync();

            ViewBag.Patrimonio = patrimonio;

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
    }
}
