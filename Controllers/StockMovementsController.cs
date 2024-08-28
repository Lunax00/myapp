using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using WebApplication1.Models;

namespace MyApp.Controllers
{
    public class StockMovementsController : Controller
    {
        private readonly MyAppContext _context;

        public StockMovementsController(MyAppContext context)
        {
            _context = context;
        }

        // GET: StockMovements
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockMovement.ToListAsync());
        }

        // GET: StockMovements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMovement = await _context.StockMovement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockMovement == null)
            {
                return NotFound();
            }

            return View(stockMovement);
        }

        // GET: StockMovements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovementDate,ProductId,Quantity,MovementType")] StockMovement stockMovement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockMovement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockMovement);
        }

        // GET: StockMovements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMovement = await _context.StockMovement.FindAsync(id);
            if (stockMovement == null)
            {
                return NotFound();
            }
            return View(stockMovement);
        }

        // POST: StockMovements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovementDate,ProductId,Quantity,MovementType")] StockMovement stockMovement)
        {
            if (id != stockMovement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockMovement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockMovementExists(stockMovement.Id))
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
            return View(stockMovement);
        }

        // GET: StockMovements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockMovement = await _context.StockMovement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockMovement == null)
            {
                return NotFound();
            }

            return View(stockMovement);
        }

        // POST: StockMovements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockMovement = await _context.StockMovement.FindAsync(id);
            if (stockMovement != null)
            {
                _context.StockMovement.Remove(stockMovement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockMovementExists(int id)
        {
            return _context.StockMovement.Any(e => e.Id == id);
        }
    }
}
