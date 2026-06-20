using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrabajoFinalProgram3.Models;
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
        [HttpGet("saldo/{codigoCripto}")]
        public async Task<IActionResult> ObtenerSaldo(string codigoCripto)
        {
            decimal saldo = await _walletService.ObtenerSaldoAsync(codigoCripto);

            return Ok(saldo);
        }
        [HttpPost("comprar")]
        public async Task<IActionResult> Comprar([FromBody] Transaccion transaccion)
        {
            await _walletService.ComprarAsync(transaccion);

            return Ok();
        }
        [HttpPost("vender")]
        public async Task<IActionResult> Vender([FromBody] Transaccion transaccion)
        {
            await _walletService.VenderAsync(transaccion);

            return Ok();
        }
        [HttpGet("portfolio")]
        public async Task<IActionResult> ObtenerPortfolio()
        {
            var portfolio = await _walletService.ObtenerPortfolioEnPesosAsync();

            return Ok(portfolio);
        }
        [HttpGet("historial")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var historial = await _walletService.ObtenerHistorialAsync();

            return Ok(historial);
        }
    }
}
