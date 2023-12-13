using AutoParts.Models;
using AutoParts.Models.Data;
using AutoParts.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppCtx _context;

        public OrdersController(AppCtx context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int? pageNumber, string searchString, OrderSortState sortOrder = OrderSortState.UserAsc)
        {
            ViewData["CurrentFilter"] = searchString;

            var orders = from s in _context.Orders.Include(i=>i.User).Include(i => i.Spare)
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.User.Email.Contains(searchString));
            }

            ViewData["UserSort"] = sortOrder == OrderSortState.UserAsc ? OrderSortState.UserDesc : OrderSortState.UserAsc;
            ViewData["SpareSort"] = sortOrder == OrderSortState.SpareAsc ? SpareSortState.SpareDesc : OrderSortState.SpareAsc;
            ViewData["DateOfRegSort"] = sortOrder == OrderSortState.DateOfRegAsc ? OrderSortState.DateOfRegDesc : OrderSortState.DateOfRegAsc;

            orders = sortOrder switch
            {
                OrderSortState.UserDesc => orders.OrderByDescending(s => s.IdUser),
                OrderSortState.SpareAsc => orders.OrderBy(s => s.IdSpare),
                OrderSortState.SpareDesc => orders.OrderByDescending(s => s.IdSpare),
                OrderSortState.DateOfRegAsc => orders.OrderBy(s => s.DateOfReg),
                OrderSortState.DateOfRegDesc => orders.OrderByDescending(s => s.DateOfReg),
                _ => orders.OrderBy(s => s.IdUser),
            };

            var appCtx = _context.Orders;
            /*return View(await appCtx.ToListAsync());*/
            int pageSize = 3;
            return View(await PaginatedList<Order>.CreateAsync(orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult CreateAsync()
        {
            ViewData["IdUser"] = new SelectList(_context.Users.OrderBy(o=>o.Email), "Id", "Email");
            ViewData["IdSpare"] = new SelectList(_context.Spares.OrderBy(o => o.TitleSpare), "Id", "TitleSpare");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderModel model)
        {
            if (ModelState.IsValid)
            {
                Order order = new()
                {
                    IdUser = model.IdUser,
                    IdSpare = model.IdSpare,
                    DateOfReg = model.DateOfReg,
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUser"] = new SelectList(_context.Users.OrderBy(o => o.Email), "Id", "Email");
            return View(model);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,IdUser,IdSpare,DateOfReg")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppCtx.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(short id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
