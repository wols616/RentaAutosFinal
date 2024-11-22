using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTecnicasIngenieria.Models;
using System.Diagnostics;

namespace ProyectoFinalTecnicasIngenieria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DateTime fechaActual = DateTime.Now;
            string fechaFormateada = fechaActual.ToString("dddd d 'de' MMMM 'del' yyyy", new System.Globalization.CultureInfo("es-ES"));
            string fechaConComa = fechaFormateada.Substring(0, 1).ToUpper() + fechaFormateada.Substring(1);

            ViewBag.FechaActual = fechaConComa;
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
