using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTecnicasIngenieria.Models;

namespace ProyectoFinalTecnicasIngenieria.Controllers
{
    public class AutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListadoAutos()
        {
            Autos autos = new Autos();
            ViewBag.autos = autos.listar();

            return View("listaAutos");
        }

        public IActionResult AgregarAuto()
        {
            return View("agregarAuto");
        }

        public IActionResult DevolverAuto()
        {
            Clientes cliente = new Clientes();
            Autos auto = new Autos();
            ViewBag.Clientes = cliente.listarClientes();
            ViewBag.Autos = auto.listar();
            return View("devolverAuto");
        }

        public IActionResult DevolviendoAuto(int idAuto, int idCliente)
        {
            Autos auto = new Autos();
            Clientes cliente = new Clientes();
            auto.editarCampo(idAuto, "Estado", "1");
            
            Alquilado.editarCampo(idAuto, idCliente, "Devuelto", "1");

            ViewBag.Clientes = cliente.listarClientes();
            return View("devolverAuto");
        }



        [HttpGet]
        public JsonResult ObtenerAutosPorCliente(int idCliente)
        {
            Autos autos = new Autos();
            try
            {
                List<Autos> listaAutos = autos.listarAutosPorIdCliente(idCliente);
                return Json(listaAutos);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult ObtenerDetallesAlquiler(int idAuto)
        {
            Autos auto = new Autos();
            try
            {
                List<Alquilado> listaAlquilados = auto.listarAlquilerPorIdAuto(idAuto);
                return Json(listaAlquilados);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }


        public IActionResult AgregandoAutos(string Marca, string Modelo, string Placa, string Tipo, double Costo_dia)
        {
            try
            {
                Autos auto = new Autos();
                auto.Marca = Marca;
                auto.Modelo = Modelo;
                auto.Placa = Placa;
                auto.Tipo = Tipo;
                auto.Estado = "1";
                auto.Costo_dia = Costo_dia;
                auto.agregar(auto);
                ViewBag.exito = 1;
                ViewBag.autos = auto.listar();
            }
            catch
            {
                ViewBag.exito = 0;
            }

            return View("listaAutos");
        }

        public IActionResult VerAuto(int id)
        {
            Autos objListar = new Autos();
            ViewBag.autos = objListar.listar(id).FirstOrDefault();
            return View("verAuto");
        }

        public IActionResult BorrandoAuto(int id)
        {
            Autos auto = new Autos();
            Alquilado.devolverAutoBorrado(id);
            auto.eliminarAuto(id);
            ViewBag.autos = auto.listar();
            return View("listaAutos");
        }

        public IActionResult EditarAuto(int id)
        {
            Autos autos = new Autos();
            if (autos.listar(id).FirstOrDefault() == null)
            {
                ViewBag.autos = autos.listar();
                return View("listaAutos");
            }
            ViewBag.autos = autos.listar(id).FirstOrDefault();
            return View("editarAuto");
        }

        public IActionResult EditandoAuto(int idauto, string Marca, string Modelo, string Placa, string Tipo, string Estado, double Costo_dia)
        {
            Autos auto = new Autos();
            auto.idauto = idauto;
            auto.Marca = Marca;
            auto.Modelo = Modelo;
            auto.Placa = Placa;
            auto.Tipo = Tipo;
            auto.Estado = Estado;
            auto.Costo_dia = Costo_dia;
            auto.editarAuto(auto);

            ViewBag.autos = auto.listar();
            return View("listaAutos");
        }
    }
}
