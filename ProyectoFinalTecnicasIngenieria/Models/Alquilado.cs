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
    }
}
