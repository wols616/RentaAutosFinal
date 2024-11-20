using MySql.Data.MySqlClient;

namespace ProyectoFinalTecnicasIngenieria.Models
{
    public class Facturas
    {
        private string cadenaConexion = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

        public int idFactura { get; set; }
        public int idCliente { get; set; }
        public int idAuto { get; set; }
        public int idEmpleado { get; set; }
        public DateTime fecha { get; set; }
        public int Dias_rentar { get; set; }
        public double subtotal { get; set; }
        public double IVA { get; set; }
        public double total { get; set; }


        public List<Facturas> VerTodasLasFacturas()
        {
            List<Facturas> facturas = new List<Facturas>();
            string query = "SELECT * FROM Facturas";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Facturas factura = new Facturas
                        {
                            idFactura = lector.GetInt32("idfactura"),
                            idCliente = lector.GetInt32("idcliente"),
                            idAuto = lector.GetInt32("idauto"),
                            idEmpleado = lector.GetInt32("idempleado"),
                            fecha = lector.GetDateTime("Fecha"),
                            Dias_rentar = lector.GetInt32("Dias_rentar"),
                            subtotal = lector.GetDouble("Subtotal"),
                            IVA = lector.GetDouble("IVA"),
                            total = lector.GetDouble("Total")
                        };
                        facturas.Add(factura);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error al obtener las facturas: " + ex.Message);
                }
            }

            return facturas;
        }




        public Facturas VerFactura(int idFactura)
        {
            Facturas factura = null;
            string query = "SELECT * FROM Facturas WHERE idfactura = @idfactura";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idfactura", idFactura);

                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    if (lector.Read())
                    {
                        factura = new Facturas
                        {
                            idFactura = lector.GetInt32("idfactura"),
                            idCliente = lector.GetInt32("idcliente"),
                            idAuto = lector.GetInt32("idauto"),
                            idEmpleado = lector.GetInt32("idempleado"),
                            fecha = lector.GetDateTime("Fecha"),
                            Dias_rentar = lector.GetInt32("Dias_rentar"),
                            subtotal = lector.GetDouble("Subtotal"),
                            IVA = lector.GetDouble("IVA"),
                            total = lector.GetDouble("Total")
                        };
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return factura;
        }


        public int AgregarFactura(Facturas nuevaFactura)
        {
            string query = "INSERT INTO Facturas (idcliente, idauto, idempleado, Fecha, Dias_rentar, Subtotal, IVA, Total) " +
                           "VALUES (@idcliente, @idauto, @idempleado, @Fecha, @Dias_rentar, @Subtotal, @IVA, @Total); " +
                           "SELECT LAST_INSERT_ID();";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@idcliente", nuevaFactura.idCliente);
                comando.Parameters.AddWithValue("@idauto", nuevaFactura.idAuto);
                comando.Parameters.AddWithValue("@idempleado", nuevaFactura.idEmpleado);
                comando.Parameters.AddWithValue("@Fecha", nuevaFactura.fecha);
                comando.Parameters.AddWithValue("@Dias_rentar", nuevaFactura.Dias_rentar);
                comando.Parameters.AddWithValue("@Subtotal", nuevaFactura.subtotal);
                comando.Parameters.AddWithValue("@IVA", nuevaFactura.IVA);
                comando.Parameters.AddWithValue("@Total", nuevaFactura.total);

                try
                {
                    conexion.Open();
                    // Ejecutar el comando y obtener el ID insertado
                    int idFactura = Convert.ToInt32(comando.ExecuteScalar());
                    return idFactura;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al agregar la factura: " + ex.Message);
                }
            }
        }


        public void ActualizarFactura(Facturas facturaActualizada)
        {
            string query = "UPDATE Facturas SET idcliente = @idcliente, idauto = @idauto, idempleado = @idempleado, " +
                           "Fecha = @Fecha, Dias_rentar = @Dias_rentar, Subtotal = @Subtotal, IVA = @IVA, Total = @Total WHERE idfactura = @idfactura";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@idcliente", facturaActualizada.idCliente);
                comando.Parameters.AddWithValue("@idauto", facturaActualizada.idAuto);
                comando.Parameters.AddWithValue("@idempleado", facturaActualizada.idEmpleado);
                comando.Parameters.AddWithValue("@Fecha", facturaActualizada.fecha);
                comando.Parameters.AddWithValue("@Dias_rentar", facturaActualizada.Dias_rentar);
                comando.Parameters.AddWithValue("@Subtotal", facturaActualizada.subtotal);
                comando.Parameters.AddWithValue("@IVA", facturaActualizada.IVA);
                comando.Parameters.AddWithValue("@Total", facturaActualizada.total);
                comando.Parameters.AddWithValue("@idfactura", facturaActualizada.idFactura);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar la factura: " + ex.Message);
                }
            }
        }

        public void EliminarFactura(int idFactura)
        {
            string query = "DELETE FROM Facturas WHERE idfactura = @idfactura";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                // Asignar valor al parámetro
                comando.Parameters.AddWithValue("@idfactura", idFactura);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar la factura: " + ex.Message);
                }
            }
        }
    }
}
