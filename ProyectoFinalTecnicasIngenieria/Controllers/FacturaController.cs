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
            Facturas facturas = objListar.VerFactura(id);

            return View("verFactura");
        }




        public IActionResult AgregarFacturass(int idCliente, int idAuto, int idEmpleado, DateTime fecha, double subtotal, double iva, double total)
        {
            try
            {
                Facturas factura = new Facturas();
                factura.idCliente = idCliente;
                factura.idAuto = idAuto;
                factura.idEmpleado = idEmpleado;
                factura.fecha = fecha;
                factura.subtotal = subtotal;
                factura.IVA = iva;
                factura.total = total;
                factura.AgregarFactura(factura);
                ViewBag.exito = 1;
                ViewBag.facturas = factura.VerTodasLasFacturas();
            }
            catch
            {
                ViewBag.exito = 0;
            }

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



        public IActionResult EliminarFactura(int idFactura)
        {
            try
            {
                Facturas factura = new Facturas();
                factura.EliminarFactura(idFactura);

                ViewBag.exito = 1;
            }
            catch (Exception ex)
            {
                ViewBag.exito = 0;
                ViewBag.errorMensaje = ex.Message;
            }

            return RedirectToAction("listaFacturas");
        }
    }
}
