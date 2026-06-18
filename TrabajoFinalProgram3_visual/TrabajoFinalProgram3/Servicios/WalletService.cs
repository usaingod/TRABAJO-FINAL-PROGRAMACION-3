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
            // Obtener las criptomonedas distintas que existen en las transacciones
            var criptomonedas = await _context.Transacciones
                .Select(t => t.CodigoCripto)
                .Distinct()
                .ToListAsync();

            List<SaldoCripto> portfolio = new();

            foreach (var cripto in criptomonedas)
            {
                decimal saldo = await ObtenerSaldoAsync(cripto);

                if (saldo > 0)
                {
                    portfolio.Add(new SaldoCripto
                    {
                        CodigoCripto = cripto,
                        Cantidad = saldo
                    });
                }
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

    }
}
