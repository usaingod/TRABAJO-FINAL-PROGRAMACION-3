namespace TrabajoFinalProgram3.Models
{
    public class Cotizacion
    {
        public string Criptomoneda { get; set; } = string.Empty;

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public string Exchange { get; set; } = string.Empty;
    }
}
