using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTecnicasIngenieria.Models;

namespace ProyectoFinalTecnicasIngenieria.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult listadoClientes()
        {
            Clientes conexion = new Clientes();
            ViewBag.Clientes = conexion.listarClientes();

            // Verifica si la lista es null y crea una lista vacía en caso de serlo
            if (ViewBag.Clientes == null)
            {
                ViewBag.Clientes = new List<Clientes>();
            }

            return View("verCliente");
        }



        public IActionResult editarCliente(int id)
        {
            Clientes conexion = new Clientes();
            ViewBag.datosCliente = conexion.listarCliente(id).FirstOrDefault();
            ViewBag.Clientes = conexion.listarClientes();
            return View("editarCliente");
        }


        public IActionResult eliminando(int idcliente)
        {
            try
            {
                Clientes objConexion = new Clientes();

                objConexion.eliminar(idcliente);
                ViewBag.Clientes = objConexion.listarClientes();
                ViewBag.exito = 1;
            }
            catch (Exception ex)
            {
                ViewBag.exito = 0;
            }
            return View("verCliente");
        }

        public IActionResult editando(string Direccion, string DUI, string Email, string Nombre, string Telefono, int idcliente)
        {
            try
            {
                Clientes conexion = new Clientes();
                conexion.editar(Direccion, DUI, Email, Nombre, Telefono, idcliente);
                ViewBag.exito = 1;
            }
            catch (Exception ex)
            {
                ViewBag.exito = 0;
            }

            return RedirectToAction("verCliente");
        }


        public IActionResult Crear(string Direccion, string DUI, string Email, string Nombre, string Telefono)
        {
            try
            {
                // Crear objeto de conexión
                Clientes conexion = new Clientes();

                // Llamar a la función para agregar la nota (INSERT)
                conexion.agregar(Direccion, DUI, Email, Nombre, Telefono);

                // Si todo sale bien, mostrar un mensaje de éxito
                ViewBag.exito = 1;
                ViewBag.clientes = conexion.listarClientes();
            }
            catch (Exception ex)
            {
                // Si ocurre un error, mostrar mensaje de error
                ViewBag.exito = 0;
            }

            // Regresar a la vista 'Agregar' (si quieres redirigir a otra vista, puedes cambiar esto)
            return View("verCliente");
        }

        public IActionResult agregar()
        {
            return View("agregarCliente");
        }

        public IActionResult verCliente()
        {
            Clientes conexion = new Clientes();
            ViewBag.Clientes = conexion.listarClientes();
            return View();
        }
    }
}
