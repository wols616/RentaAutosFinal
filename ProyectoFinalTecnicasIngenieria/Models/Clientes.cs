using MySql.Data.MySqlClient;

namespace ProyectoFinalTecnicasIngenieria.Models
{
    public class Clientes
    {
        private string cadenamysql = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

        public int idcliente { get; set; }
        public string Direccion { get; set; }
        public string DUI { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public List<Clientes> listarClientes()
        {
            List<Clientes> listaClientes = new List<Clientes>();
            string query = "SELECT idcliente,Nombre,Telefono,DUI,Email,Direccion FROM Clientes;";

            using (MySqlConnection conexion = new MySqlConnection(cadenamysql))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Clientes cliente = new Clientes();
                        cliente.idcliente = lector.GetInt16(0);
                        cliente.Nombre = lector[1].ToString();
                        cliente.Telefono = lector[2].ToString();
                        cliente.DUI = lector[3].ToString();
                        cliente.Email = lector[4].ToString();
                        cliente.Direccion = lector[5].ToString();

                        listaClientes.Add(cliente);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaClientes;
        }

        public void eliminar(int idcliente)
        {
            string query = "DELETE FROM Clientes WHERE idcliente = @idcliente;";

            using (MySqlConnection conexion = new MySqlConnection(cadenamysql))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("idcliente", idcliente);
                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error: " + ex.Message);
                }
            }
        }

        public List<Clientes> listarCliente(int idcliente)
        {
            List<Clientes> listaDatosCliente = new List<Clientes>();
            string query = "SELECT idcliente,Nombre,Telefono,DUI,Email,Direccion FROM Clientes WHERE idcliente = @id;";

            using (MySqlConnection conexion = new MySqlConnection(cadenamysql))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", idcliente);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Clientes cliente = new Clientes();
                        cliente.idcliente = lector.GetInt16(0);
                        cliente.Nombre = lector[1].ToString();
                        cliente.Telefono = lector[2].ToString();
                        cliente.DUI = lector[3].ToString();
                        cliente.Email = lector[4].ToString();
                        cliente.Direccion = lector[5].ToString();

                        listaDatosCliente.Add(cliente);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaDatosCliente;
        }

        public void editar(string Direccion, string DUI, string Email, string Nombre, string Telefono, int idcliente)
        {
            string query = "UPDATE Clientes SET Direccion = @Direccion, DUI = @DUI, Email = @Email, " +
                           "Nombre = @Nombre, Telefono = @Telefono WHERE idcliente = @idcliente";

            using (MySqlConnection conexion = new MySqlConnection(cadenamysql))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Direccion", Direccion);
                comando.Parameters.AddWithValue("@DUI", DUI);
                comando.Parameters.AddWithValue("@Email", Email);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                comando.Parameters.AddWithValue("@idcliente", idcliente);
                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error: " + ex.Message);
                }

            }
        }

        public void agregar(string Direccion, string DUI, string Email, string Nombre, string Telefono)
        {
            string query = "INSERT INTO Clientes(Direccion,DUI, Email, Nombre,Telefono) VALUES(@Direccion,@DUI, @Email, @Nombre,@Telefono)";

            using (MySqlConnection conexion = new MySqlConnection(cadenamysql))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Direccion", Direccion);
                comando.Parameters.AddWithValue("@DUI", DUI);
                comando.Parameters.AddWithValue("@Email", Email);
                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@Telefono", Telefono);
                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error: " + ex.Message);
                }
            }
        }
    }
}
