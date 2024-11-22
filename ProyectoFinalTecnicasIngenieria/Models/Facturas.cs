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

        public Facturas VerFactura(int idfactura)
        {
            Facturas factura = new Facturas();
            string query = "SELECT * FROM Facturas WHERE idfactura = @idfactura";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idfactura", idfactura);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
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
                    throw new Exception("Hay un error al obtener las facturas: " + ex.Message);
                }
            }

            return factura;
        }

        public Clientes ListarClienteDeFactura(int idFactura)
        {
            Clientes cliente = null;
            string query = "SELECT Clientes.idcliente, Clientes.Nombre, Clientes.Direccion, " +
                "Clientes.Telefono, Clientes.DUI, Clientes.Email FROM Facturas " +
                "JOIN Clientes ON Clientes.idcliente = Facturas.idcliente " +
                "WHERE Facturas.idfactura = @idfactura";

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
                        cliente = new Clientes
                        {
                            idcliente = lector.GetInt32("idcliente"),
                            Nombre = lector.GetString("Nombre"),
                            Direccion = lector.GetString("Direccion"),
                            Telefono = lector.GetString("Telefono"),
                            DUI = lector.GetString("DUI"),
                            Email = lector.GetString("Email")

                        };
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return cliente;
        }

        public Alquilado ListarAlquiladosFactura(int idFactura)
        {
            Alquilado alquilado = null;
            string query = "SELECT Alquilados.idalquiler, Alquilados.idauto, Alquilados.idcliente, " +
                "Alquilados.idempleado, Alquilados.idfactura, Alquilados.Fecha, " +
                "Alquilados.Fecha_devolver, Alquilados.Devuelto FROM Facturas " +
                "JOIN Alquilados ON Alquilados.idfactura = Facturas.idfactura " +
                "WHERE Facturas.idfactura = @idfactura";

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
                        alquilado = new Alquilado
                        {
                            idalquiler = lector.GetInt32("idalquiler"),
                            idauto = lector.GetInt32("idauto"),
                            idcliente = lector.GetInt32("idcliente"),
                            idempleado = lector.GetInt32("idempleado"),
                            idfactura = lector.GetInt32("idfactura"),
                            Fecha = lector.GetDateTime("Fecha"),
                            Fecha_devolver = lector.GetDateTime("Fecha_devolver"),
                            Devuelto = lector.GetString("Devuelto")

                        };
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return alquilado;
        }

        public Autos ListarAutoDeFactura(int idFactura)
        {
            Autos auto = null;
            string query = "SELECT Autos.idauto, Marca, Modelo, Placa, Tipo, Estado, Costo_dia FROM Alquilados " +
                "JOIN Autos ON Autos.idauto = Alquilados.idauto " +
                "WHERE Alquilados.idfactura = @idFactura";

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idFactura", idFactura);

                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    if (lector.Read())
                    {
                        auto = new Autos
                        {
                            idauto = lector.GetInt32(0),
                            Marca = lector.GetString(1),
                            Modelo = lector.GetString(2),
                            Placa = lector.GetString(3),
                            Tipo = lector.GetString(4),
                            Estado = lector.GetString(5),
                            Costo_dia = lector.GetDouble(6)

                        };
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return auto;
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
