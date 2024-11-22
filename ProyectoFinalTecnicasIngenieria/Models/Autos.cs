using MySql.Data.MySqlClient;

namespace ProyectoFinalTecnicasIngenieria.Models
{
    public class Autos
    {
        private string cadena = "Server=atids.online;Database=atidsuser_DriveNow;Uid=atidsuser_DriveNow;Pwd=contraseña123";

        public int idauto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public double Costo_dia { get; set; }

        public List<Autos> listar()
        {
            List<Autos> listaAutos = new List<Autos>();
            string query = "select * from Autos";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Autos auto = new Autos();
                        auto.idauto = lector.GetInt32(0);
                        auto.Marca = lector.GetString(1);
                        auto.Modelo = lector.GetString(2);
                        auto.Placa = lector.GetString(3);
                        auto.Tipo = lector.GetString(4);
                        auto.Estado = lector.GetString(5);
                        auto.Costo_dia = lector.GetDouble(6);
                        listaAutos.Add(auto);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaAutos;
        }

        public List<Autos> listar(int idAuto)
        {
            List<Autos> listaAutos = new List<Autos>();
            string query = "select * from Autos WHERE idauto = @idauto";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idauto", idAuto);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Autos auto = new Autos();
                        auto.idauto = lector.GetInt32(0);
                        auto.Marca = lector.GetString(1);
                        auto.Modelo = lector.GetString(2);
                        auto.Placa = lector.GetString(3);
                        auto.Tipo = lector.GetString(4);
                        auto.Estado = lector.GetString(5);
                        auto.Costo_dia = lector.GetDouble(6);
                        listaAutos.Add(auto);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaAutos;
        }

        public List<Autos> listarAutosPorIdCliente(int idCliente)
        {
            List<Autos> listaAutos = new List<Autos>();
            string query = "select Facturas.idauto, Marca, Modelo, Placa, Tipo, Estado, Costo_dia from Facturas " +
                "JOIN Clientes ON Clientes.idcliente = Facturas.idcliente " +
                "JOIN Autos ON Autos.idauto = Facturas.idauto " +
                "JOIN Alquilados ON Alquilados.idfactura = Facturas.idfactura " +
                "where Clientes.idcliente = @idCliente AND Alquilados.Devuelto = 0";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idCliente", idCliente);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Autos auto = new Autos();
                        auto.idauto = lector.GetInt32(0);
                        auto.Marca = lector.GetString(1);
                        auto.Modelo = lector.GetString(2);
                        auto.Placa = lector.GetString(3);
                        auto.Tipo = lector.GetString(4);
                        auto.Estado = lector.GetString(5);
                        auto.Costo_dia = lector.GetDouble(6);
                        listaAutos.Add(auto);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaAutos;
        }

        public List<Alquilado> listarAlquilerPorIdAuto(int idAuto)
        {
            List<Alquilado> listaAlquilado= new List<Alquilado>();
            string query = "SELECT Alquilados.idalquiler, Autos.idauto, idcliente, idempleado, idfactura, " +
                "Alquilados.Fecha, Alquilados.Fecha_devolver " +
                "FROM Alquilados JOIN Autos ON Autos.idauto = Alquilados.idauto WHERE Autos.idauto = @idAuto";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idAuto", idAuto);
                try
                {
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Alquilado alquilado = new Alquilado();
                        alquilado.idalquiler = lector.GetInt32(0);
                        alquilado.idauto = lector.GetInt32(1);
                        alquilado.idcliente = lector.GetInt32(2);
                        alquilado.idempleado = lector.GetInt32(3);
                        alquilado.idfactura = lector.GetInt32(4);
                        alquilado.Fecha = DateTime.Parse(lector.GetDateTime(5).ToString("dd/MM/yyyy"));
                        alquilado.Fecha_devolver = DateTime.Parse(lector.GetDateTime(6).ToString("dd/MM/yyyy"));
                        listaAlquilado.Add(alquilado);
                    }
                    lector.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hay un error: " + ex.Message);
                }
            }
            return listaAlquilado;
        }

        public void agregar(Autos auto)
        {
            string query = "insert into Autos(Marca, Modelo, Placa, Tipo, Estado, Costo_dia) values" + "(@Marca, @Modelo, @Placa, @Tipo, @Estado, @Costo_dia)";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Marca", auto.Marca);
                comando.Parameters.AddWithValue("@Modelo", auto.Modelo);
                comando.Parameters.AddWithValue("@Placa", auto.Placa);
                comando.Parameters.AddWithValue("@Tipo", auto.Tipo);
                comando.Parameters.AddWithValue("@Estado", auto.Estado);
                comando.Parameters.AddWithValue("@Costo_dia", auto.Costo_dia);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un error" + ex.Message); }

            }
        }

        public void editarAuto(Autos auto)
        {
            string query = "update Autos set Marca=@Marca, Modelo=@Modelo, Placa=@Placa, " +
                "Tipo=@Tipo, Estado=@Estado, Costo_dia=@Costo_dia " + " where idauto=@idauto";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Marca", auto.Marca);
                comando.Parameters.AddWithValue("@Modelo", auto.Modelo);
                comando.Parameters.AddWithValue("@Placa", auto.Placa);
                comando.Parameters.AddWithValue("@Tipo", auto.Tipo);
                comando.Parameters.AddWithValue("@Estado", auto.Estado);
                comando.Parameters.AddWithValue("@Costo_dia", auto.Costo_dia);
                comando.Parameters.AddWithValue("@idauto", auto.idauto);


                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un erro" + ex.Message); }
            }
        }

        public void editarCampo(int idauto, string nombreCampo, string dato)
        {
            string query = $"update Autos set {nombreCampo} = {dato} where idauto=@idauto";
            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idauto", idauto);

                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex) { throw new Exception("Hubo un erro" + ex.Message); }
            }
        }

        public void eliminarAuto(int idauto)
        {
            string query = "delete from Autos where idauto=@idauto";

            using (MySqlConnection conexion = new MySqlConnection(cadena))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@idauto", idauto);

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
