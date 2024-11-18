using MySql.Data.MySqlClient;

namespace ProyectoFinalTecnicasIngenieria.Models
{
    public class Empleados
    {
        private string cadena = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

        public int idempleado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }

        public List<Empleados> listar()
        { 
            List<Empleados> listaEmpleados = new List<Empleados>();
            string query = "select * from Empleados";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Empleados empleado = new Empleados();
                        empleado.idempleado = lector.GetInt32(0);
                        empleado.Nombre = lector.GetString(1);
                        empleado.Telefono = lector.GetString(2);
                        empleado.Cargo = lector.GetString(3);
                        empleado.Email = lector.GetString(4);
                        listaEmpleados.Add(empleado);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaEmpleados;
        }

        public List<Empleados> listar(int idEmpleado)
        {
            List<Empleados> listaEmpleados = new List<Empleados>();
            string query = "select * from Empleados WHERE idempleado = @idempleado";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idempleado", idEmpleado);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Empleados empleado = new Empleados();
                        empleado.idempleado = lector.GetInt32(0);
                        empleado.Nombre = lector.GetString(1);
                        empleado.Telefono = lector.GetString(2);
                        empleado.Cargo = lector.GetString(3);
                        empleado.Email = lector.GetString(4);

                        listaEmpleados.Add(empleado);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaEmpleados;
        }


        public void AgregarEmpleado(Empleados nuevoEmpleado)
        {
            string query = "INSERT INTO Empleados (Nombre, Telefono, Cargo, Email) " +
                           "VALUES (@Nombre, @Telefono, @Cargo, @Email)";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@Nombre", nuevoEmpleado.Nombre);
                comando.Parameters.AddWithValue("@Telefono", nuevoEmpleado.Telefono);
                comando.Parameters.AddWithValue("@Cargo", nuevoEmpleado.Cargo);
                comando.Parameters.AddWithValue("@Email", nuevoEmpleado.Email);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al agregar el empleado: " + ex.Message);
                }
            }
        }

        public void editarEmpleado(Empleados empleado)
        {
            string query = "update Empleados set Nombre=@Nombre, Telefono=@Telefono, Cargo=@Cargo, " +
                "Email=@Email " + " where idempleado=@idempleado";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                comando.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                comando.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                comando.Parameters.AddWithValue("@Email", empleado.Email);
                comando.Parameters.AddWithValue("@idempleado", empleado.idempleado);


                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un erro" + ex.Message); }
            }
        }

        public void eliminarEmpleado(int idempleado)
        {
            string query = "delete from Empleados where idempleado=@idempleado";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@idempleado", idempleado);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
