namespace ClienteTransacciones.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
    }
}
