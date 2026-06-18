using System.Text.Json.Serialization;

namespace TrabajoFinalProgram3.Models
{
    public class RespuestaBelo
    {
        [JsonPropertyName("ask")]
        public decimal PrecioCompra { get; set; }

        [JsonPropertyName("bid")]
        public decimal PrecioVenta { get; set; }
    }
}
