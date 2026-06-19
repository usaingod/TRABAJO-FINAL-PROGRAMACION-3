using TrabajoFinalProgram3.Data;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalProgram3.Models;

namespace TrabajoFinalProgram3.Services
{
    public class WalletService
    {
        private readonly AppDbContext _context;
        private readonly CriptoYa _criptoYa;

        public WalletService(AppDbContext context,
                             CriptoYa criptoYa)
        {
            _context = context;
            _criptoYa = criptoYa;
        }
        public async Task<decimal> ObtenerSaldoAsync(string codigoCripto)
        {
            // Compras
            decimal compras = await _context.Transacciones
                .Where(t => t.CodigoCripto == codigoCripto &&
                            t.Accion == "Compra")
                .SumAsync(t => t.CantidadCripto);

            // Ventas
            decimal ventas = await _context.Transacciones
                .Where(t => t.CodigoCripto == codigoCripto &&
                            t.Accion == "Venta")
                .SumAsync(t => t.CantidadCripto);

            return compras - ventas;
        }
        public async Task<List<SaldoCripto>> ObtenerPortfolioAsync()
        {
            // Criptomonedas soportadas por la aplicación
            List<string> criptomonedas = new()
            {
                "btc",
                "eth",
                "usdc",
                "sol"
            };

            List<SaldoCripto> portfolio = new();

            foreach (var cripto in criptomonedas)
            {
                decimal saldo = await ObtenerSaldoAsync(cripto);                
                portfolio.Add(new SaldoCripto
                {
                  CodigoCripto = cripto,
                  Cantidad = saldo
                });
                
            }

            return portfolio;
        }
        public async Task<List<PortfolioPesos>> ObtenerPortfolioEnPesosAsync()
        {
            var portfolio = await ObtenerPortfolioAsync();

            List<PortfolioPesos> portfolioPesos = new();

            foreach (var item in portfolio)
            {
                var cotizacion = await _criptoYa.ObtenerCotizacionAsync(item.CodigoCripto);

                portfolioPesos.Add(new PortfolioPesos
                {
                    CodigoCripto = item.CodigoCripto,
                    Cantidad = item.Cantidad,
                    ValorEnPesos = item.Cantidad * cotizacion.PrecioVenta
                });
            }

            return portfolioPesos;
        }
        public async Task<decimal> ObtenerPatrimonioAsync()
        {
            var portfolio = await ObtenerPortfolioEnPesosAsync();

            return portfolio.Sum(p => p.ValorEnPesos);
        }
        public async Task<Cotizacion> ObtenerCotizacionAsync(string codigoCripto)
        {
            return await _criptoYa.ObtenerCotizacionAsync(codigoCripto);
        }
        public async Task ComprarAsync(Transaccion transaccion)
        {
            var cotizacion = await _criptoYa.ObtenerCotizacionAsync(transaccion.CodigoCripto);

            transaccion.Accion = "Compra";

            transaccion.Dinero = transaccion.CantidadCripto * cotizacion.PrecioCompra;

            transaccion.FechaHora = DateTime.Now;

            _context.Transacciones.Add(transaccion);

            await _context.SaveChangesAsync();
        }
        public async Task VenderAsync(Transaccion transaccion)
        {
            decimal saldo = await ObtenerSaldoAsync(transaccion.CodigoCripto);

            if (saldo < transaccion.CantidadCripto)
            {
                throw new Exception("Saldo insuficiente para realizar la venta.");
            }

            var cotizacion = await _criptoYa.ObtenerCotizacionAsync(transaccion.CodigoCripto);

            transaccion.Accion = "Venta";

            transaccion.Dinero = transaccion.CantidadCripto * cotizacion.PrecioVenta;

            transaccion.FechaHora = DateTime.Now;

            _context.Transacciones.Add(transaccion);

            await _context.SaveChangesAsync();
        }

    }
}
