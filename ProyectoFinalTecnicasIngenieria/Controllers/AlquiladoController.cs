using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTecnicasIngenieria.Models;

namespace ProyectoFinalTecnicasIngenieria.Controllers
{
    public class AlquiladoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AgregarAlquilado()
        {
            return View("agregarAlquilado");
        }

        
    }
}
