using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoFinalProgram3.Data;
using TrabajoFinalProgram3.Models;
using TrabajoFinalProgram3.Services;

namespace TrabajoFinalProgram3.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CriptoYa _criptoYa;
        private readonly WalletService _walletService;

        public TransaccionesController(AppDbContext context, CriptoYa criptoYa, WalletService walletService)
        {
            _context = context;
            _criptoYa = criptoYa;
            _walletService = walletService;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transacciones.ToListAsync());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoCripto,Accion,CantidadCripto")] Transaccion transaccion)
        {
            //evita anidar todo el código dentro de un if gigante
            if (!ModelState.IsValid)
            {
                return View(transaccion);
            }
            // Obtener la cotización actual
            var cotizacion = await _criptoYa.ObtenerCotizacionAsync(transaccion.CodigoCripto);
            // Si es una venta, verificar que haya saldo suficiente
            if (transaccion.Accion == "Venta")
            {
                decimal saldoDisponible =
                    await _walletService.ObtenerSaldoAsync(transaccion.CodigoCripto);

                if (transaccion.CantidadCripto > saldoDisponible)
                {
                    ModelState.AddModelError("", "Saldo insuficiente para realizar la venta.");

                    return View(transaccion);
                }
            }

            // Calcular el monto según la operación
            if (transaccion.Accion == "Compra")
            {
                transaccion.Dinero = transaccion.CantidadCripto * cotizacion.PrecioCompra;
            }
            else
            {
                transaccion.Dinero = transaccion.CantidadCripto * cotizacion.PrecioVenta;
            }
            // Registrar la fecha y hora automáticamente
            transaccion.FechaHora = DateTime.Now;

            _context.Add(transaccion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }
            return View(transaccion);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoCripto,Accion,CantidadCripto,Dinero,FechaHora")] Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccionExists(transaccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaccion);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion != null)
            {
                _context.Transacciones.Remove(transaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transacciones.Any(e => e.Id == id);
        }
    }
}
