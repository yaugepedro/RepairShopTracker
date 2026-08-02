using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairShopTracker.Web.Data;
using RepairShopTracker.Web.Models;

namespace RepairShopTracker.Web.Controllers
{
    public class RepairOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepairOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Filtro simple: si no hay sesión, manda al login
        private bool IsLoggedIn() => HttpContext.Session.GetString("Username") != null;

        // GET: RepairOrders (Read - listado)
        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var orders = await _context.RepairOrders.ToListAsync();
            return View(orders);
        }

        // GET: RepairOrders/Create
        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View();
        }

        // POST: RepairOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepairOrder order)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RepairOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var order = await _context.RepairOrders.FindAsync(id);
            if (order == null) return NotFound();

            return View(order);
        }

        // POST: RepairOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RepairOrder order)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id != order.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RepairOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id == null) return NotFound();

            var order = await _context.RepairOrders.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) return NotFound();

            return View(order);
        }

        // POST: RepairOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var order = await _context.RepairOrders.FindAsync(id);
            if (order != null)
            {
                _context.RepairOrders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}