using System.Net.Http;
using System.Net.Http.Json;
using TrabajoFinalProgram3.Models;

namespace TrabajoFinalProgram3.Services
{
    public class CriptoYa
    {
        private readonly HttpClient _httpClient;

        public CriptoYa(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Cotizacion> ObtenerCotizacionAsync(string codigoCripto)
        {
            codigoCripto = codigoCripto.ToLower();

            string url = $"https://criptoya.com/api/belo/{codigoCripto}/ars/1";

            RespuestaBelo? respuesta =
                await _httpClient.GetFromJsonAsync<RespuestaBelo>(url);

            if (respuesta == null)
            {
                throw new Exception("No fue posible obtener la cotización.");
            }

            return new Cotizacion
            {
                Criptomoneda = codigoCripto.ToUpper(),
                PrecioCompra = respuesta.PrecioCompra,
                PrecioVenta = respuesta.PrecioVenta,
                Exchange = "Belo"
            };
        }
    }



}