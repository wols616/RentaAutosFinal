using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTecnicasIngenieria.Models;

namespace ProyectoFinalTecnicasIngenieria.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListadoEmpleados()
        {
            Empleados empleado = new Empleados();
            ViewBag.empleado = empleado.listar();

            return View("listaEmpleado");
        }

        public IActionResult VerEmpleado(int id)
        {
            Empleados objListar = new Empleados();
            ViewBag.empleado = objListar.listar(id).FirstOrDefault();
            return View("verEmpleado");
        }

        public IActionResult AgregarEmpleado()
        {
            return View("agregarEmpleado");
        }
        public IActionResult AgregandoEmpleados(string Nombre, string Telefono, string Cargo, string Email)
        {
            try
            {
                Empleados empleado = new Empleados();
                empleado.Nombre = Nombre;
                empleado.Telefono = Telefono;
                empleado.Cargo = Cargo;
                empleado.Email = Email;
                empleado.AgregarEmpleado(empleado);
                ViewBag.exito = 1;
                ViewBag.empleado = empleado.listar();
            }
            catch
            {
                ViewBag.exito = 0;
            }

            return View("listaEmpleado");
        }

        public IActionResult BorrandoEmpleados(int id)
        {
            Empleados empleado = new Empleados();
            empleado.eliminarEmpleado(id);
            ViewBag.empleado = empleado.listar();
            return View("listaEmpleado");
        }

        public IActionResult EditarEmpleados(int id)
        {
            Empleados empleado = new Empleados();
            if (empleado.listar(id).FirstOrDefault() == null)
            {
                ViewBag.empleados = empleado.listar();
                return View("listaEmpleado");
            }
            ViewBag.empleados = empleado.listar(id).FirstOrDefault();
            return View("editarEmpleado");
        }


        public IActionResult EditandoEmpleado(int idEmpleado, string Nombre, string Telefono, string Cargo, string Email)
        {
            Empleados empleado = new Empleados();
            empleado.idempleado = idEmpleado;
            empleado.Nombre = Nombre;
            empleado.Telefono = Telefono;
            empleado.Cargo = Cargo;
            empleado.Email = Email;

            empleado.editarEmpleado(empleado);

            ViewBag.empleado = empleado.listar();
            return View("listaEmpleado");
        }
    }
}
