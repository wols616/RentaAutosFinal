using ProyectoFinalTecnicasIngenieria.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalTecnicasIngenieria.Controllers
{
    public class FacturaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListarFactura()
        {
            Facturas facturas = new Facturas();
            ViewBag.facturas = facturas.VerTodasLasFacturas();
            return View("listaFacturas");
        }

        public IActionResult VerFactura(int id)
        {
            Facturas objListar = new Facturas();
            //Clientes clientes = objListar.ListarClienteDeFactura(id);
            ViewBag.Cliente = objListar.ListarClienteDeFactura(id);
            ViewBag.Alquilado = objListar.ListarAlquiladosFactura(id);
            ViewBag.Auto = objListar.ListarAutoDeFactura(id);
            ViewBag.Factura = objListar.VerFactura(id);
            return View("verFactura");
        }

        public IActionResult AgregarFactura()
        {
            Clientes cliente = new Clientes();
            Autos auto = new Autos();
            Empleados empleados = new Empleados();
            ViewBag.Clientes = cliente.listarClientes();
            ViewBag.Autos = auto.listar();
            ViewBag.Empleados = empleados.listar();
            return View("agregarFactura");
        }

        public IActionResult AgregandoFactura(int idCliente, int idAuto, int idEmpleado, DateTime fecha, int dias_rentar)
        {
            Facturas factura = new Facturas();
            Autos autos = new Autos();
            int idFactura = 0;
            try
            {
                factura.idCliente = idCliente;
                factura.idAuto = idAuto;
                factura.idEmpleado = idEmpleado;
                factura.fecha = fecha;
                factura.Dias_rentar = dias_rentar;

                double subtotal = (autos.listar(idAuto).FirstOrDefault().Costo_dia) * dias_rentar;
                factura.subtotal = subtotal;
                double iva = subtotal * 0.13;
                factura.IVA = Math.Round(iva, 2) ;
                factura.total = subtotal + iva;
                idFactura = factura.AgregarFactura(factura);
                ViewBag.exito = 1;
                ViewBag.facturas = factura.VerTodasLasFacturas();
            }
            catch
            {
                ViewBag.exito = 0;
            }
            
            Alquilado alquilado = new Alquilado(idAuto, idCliente, idEmpleado, idFactura, DateTime.Now, DateTime.Now.AddDays(dias_rentar));
            int idalquilado = alquilado.agregarAlquilado(alquilado);
            autos.editarCampo(idAuto, "Estado", "0");
            return View("listaFacturas");
        }

        public IActionResult ActualizarFacturass(int idFactura, int idCliente, int idAuto, int idEmpleado, DateTime fecha, double subtotal, double iva, double total)
        {
            try
            {
                Facturas factura = new Facturas();
                Facturas facturaActualizada = new Facturas
                {
                    idFactura = idFactura,
                    idCliente = idCliente,
                    idAuto = idAuto,
                    idEmpleado = idEmpleado,
                    fecha = fecha,
                    subtotal = subtotal,
                    IVA = iva,
                    total = total
                };

                factura.ActualizarFactura(facturaActualizada);
                ViewBag.exito = 1;
                ViewBag.facturas = factura.VerTodasLasFacturas();
            }
            catch
            {
                ViewBag.exito = 0;
            }
            return View("listaFacturas");
        }



        public IActionResult EliminarFactura(int id)
        {
            Facturas factura = new Facturas();
            Autos auto = new Autos();
            Alquilado alquilado = new Alquilado();

            try
            {
                // Intentar listar el auto y editar su campo
                try
                {
                    auto = factura.ListarAutoDeFactura(id);
                    if (auto != null) // Verificar si el auto existe
                    {
                        auto.editarCampo(auto.idauto, "Estado", "1");
                    }
                }
                catch (Exception ex)
                {
                    // Registrar el error, pero permitir que la ejecución continúe
                    Console.WriteLine($"Error al listar o editar el auto: {ex.Message}");
                }

                // Editar el campo de "Devuelto" aunque falle lo anterior
                try
                {
                    alquilado.editarCampo(id, "Devuelto", "1");
                }
                catch (Exception ex)
                {
                    // Registrar el error, pero permitir que la ejecución continúe
                    Console.WriteLine($"Error al editar campo 'Devuelto': {ex.Message}");
                }

                // Finalmente, eliminar la factura
                factura.EliminarFactura(id);

                ViewBag.exito = 1;
            }
            catch (Exception ex)
            {
                ViewBag.exito = 0;
                ViewBag.errorMensaje = ex.Message;
            }

            ViewBag.facturas = factura.VerTodasLasFacturas();
            return View("listaFacturas");
        }

    }
}
