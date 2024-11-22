using MySql.Data.MySqlClient;

namespace ProyectoFinalTecnicasIngenieria.Models
{
    public class Alquilado
    {
        private string cadena = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

        public Alquilado(int idauto, int idcliente, int idempleado, int idfactura, DateTime fecha, DateTime fecha_devolver)
        {
            this.idauto = idauto;
            this.idcliente = idcliente;
            this.idempleado = idempleado;
            this.idfactura = idfactura;
            Fecha = fecha;
            Fecha_devolver = fecha_devolver;
        }
        public Alquilado() { }

        public int idalquiler { get; set; }
        public int idauto { get; set; }
        public int idcliente { get; set; }
        public int idempleado { get; set; }
        public int idfactura { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Fecha_devolver { get; set; }
        public string Devuelto { get; set; }

        public int agregarAlquilado(Alquilado alquilado)
        {
            string query = "insert into Alquilados(idauto, idcliente, idempleado, idfactura, Fecha, Fecha_devolver, Devuelto) " +
                           "values(@idauto, @idcliente, @idempleado, @idfactura, @Fecha, @Fecha_devolver, 0); " +
                           "SELECT LAST_INSERT_ID();";  // Obtiene el ID recién insertado

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idauto", alquilado.idauto);
                comando.Parameters.AddWithValue("@idcliente", alquilado.idcliente);
                comando.Parameters.AddWithValue("@idempleado", alquilado.idempleado);
                comando.Parameters.AddWithValue("@idfactura", alquilado.idfactura);
                comando.Parameters.AddWithValue("@Fecha", alquilado.Fecha);
                comando.Parameters.AddWithValue("@Fecha_devolver", alquilado.Fecha_devolver);

                try
                {
                    conexion.Open();
                    int idRecienCreado = Convert.ToInt32(comando.ExecuteScalar());  // Ejecuta el query y obtiene el ID
                    conexion.Close();
                    return idRecienCreado;  // Retorna el ID recién creado
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error: " + ex.Message);
                }
            }
        }

        public Alquilado ListarAlquilerPorFactura(int idFactura)
        {
            Alquilado alquilado = null;
            string query = "SELECT * FROM Alquilados " +
                "WHERE Alquilados.idfactura = @idFactura";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idFactura", idFactura);

                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    if (lector.Read())
                    {
                        alquilado = new Alquilado
                        {
                            idalquiler = lector.GetInt32(0),
                            idauto = lector.GetInt32(1),
                            idcliente = lector.GetInt32(2),
                            idempleado = lector.GetInt32(3),
                            idfactura = lector.GetInt32(4),
                            Fecha = lector.GetDateTime(5),
                            Fecha_devolver = lector.GetDateTime(6),
                            Devuelto = lector.GetString(7)

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

        public void editarCampo(int idFactura, string nombreCampo, string dato)
        {
            string query = $"update Alquilados set {nombreCampo} = {dato} where idfactura=@idFactura";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idFactura", idFactura);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un erro" + ex.Message); }
            }
        }

        public static void devolverAutoBorrado(int idAuto)
        {
            string cadena = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

            string query = $"update Alquilados set Devuelto = 1 where idauto=@idAuto";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idAuto", idAuto);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un erro" + ex.Message); }
            }
        }

        public static void devolverAutoClienteBorrado(int idCliente)
        {
            string cadena = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

            // Consultas para actualizar los campos en ambas tablas
            string queryAlquilados = $"UPDATE Alquilados SET Devuelto = 1 WHERE idcliente = @idCliente";
            string queryAutos = $"UPDATE Autos SET Estado = 1 WHERE idauto IN (SELECT idauto FROM Alquilados WHERE idcliente = @idCliente)";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comandoAlquilados = new MySqlCommand(queryAlquilados, conexion);
                MySqlCommand comandoAutos = new MySqlCommand(queryAutos, conexion);

                comandoAlquilados.Parameters.AddWithValue("@idCliente", idCliente);
                comandoAutos.Parameters.AddWithValue("@idCliente", idCliente);

                try
                {
                    conexion.Open();

                    // Ejecutar ambas consultas
                    comandoAlquilados.ExecuteNonQuery();
                    comandoAutos.ExecuteNonQuery();

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error: " + ex.Message);
                }
            }
        }


        public static void editarCampo(int idauto, int idcliente, string nombreCampo, string dato)
        {
            string cadena = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

            string query = $"update Alquilados set {nombreCampo} = {dato} where idauto=@idauto AND idcliente=@idcliente";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idauto", idauto);
                comando.Parameters.AddWithValue("@idcliente", idcliente);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un erro" + ex.Message); }
            }
        }

        
    }
}
