namespace TrabajoFinalProgram3.Models
{
    public class Transaccion
    {
        public int Id { get; set; }

        public string CodigoCripto { get; set; } = string.Empty;

        public string Accion { get; set; } = string.Empty;

        public decimal CantidadCripto { get; set; }

        public decimal Dinero { get; set; }

        public DateTime FechaHora { get; set; }
    }
}
