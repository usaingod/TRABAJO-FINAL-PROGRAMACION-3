using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinalProgram3.Services;

namespace TrabajoFinalProgram3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletApiController : ControllerBase
    {
        private readonly WalletService _walletService;

        public WalletApiController(WalletService walletService)
        {
            _walletService = walletService;
        }
        [HttpGet("patrimonio")]
        public async Task<ActionResult<decimal>> ObtenerPatrimonio()
        {
            decimal patrimonio = await _walletService.ObtenerPatrimonioAsync();

            return Ok(Math.Round(patrimonio, 2));
        }
        [HttpGet("cotizacion/{codigoCripto}")]
        public async Task<IActionResult> ObtenerCotizacion(string codigoCripto)
        {
            var cotizacion = await _walletService.ObtenerCotizacionAsync(codigoCripto);

            return Ok(cotizacion);
        }
    }
}
