using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerGamesShop.Models;

namespace ComputerGamesShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ComputerGamesShopContext _context;

        public OrdersController(ComputerGamesShopContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var computerGamesShopContext = _context.Order.Include(o => o.Customer).Include(o => o.Store);
            return View(await computerGamesShopContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetails =
                _context.Order
                    .Join(_context.OrderItems,
                            order => order.OrderID,
                            items => items.orderId,
                            (order, items) => new { order, items })
                    .Join(_context.Game,
                            orderAndItems => orderAndItems.items.gameId,
                            game => game.ID,
                            (orderAndItems, game) => new OrderDetails
                            {
                                OrderId = orderAndItems.order.OrderID,
                                StoreName = orderAndItems.order.Store.DisplayName,
                                CustomerEmail = orderAndItems.order.Customer.Email,
                                OrderDate = orderAndItems.order.OrderDate.ToShortDateString(),
                                gameName = game.Title,
                                Price = game.Price
                            })
                     .Where(singleOrderDetails => singleOrderDetails.OrderId == id);



            //var orderDetails = await _context.Order
            //    .Include(o => o.Customer)
            //    .Include(o => o.Store)
            //    .SingleOrDefaultAsync(m => m.OrderID == id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.User, "UserID", "Email");
            ViewData["StoreID"] = new SelectList(_context.Store, "StoreID", "StoreCity");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerId,StoreID,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.User, "UserID", "Email", order.CustomerId);
            ViewData["StoreID"] = new SelectList(_context.Store, "StoreID", "StoreCity", order.StoreID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.User, "UserID", "Email", order.CustomerId);
            ViewData["StoreID"] = new SelectList(_context.Store, "StoreID", "StoreCity", order.StoreID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CustomerId,StoreID,OrderDate")] Order order)
        {
            if (id != order.OrderID)
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
                    if (!OrderExists(order.OrderID))
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
            ViewData["CustomerId"] = new SelectList(_context.User, "UserID", "Email", order.CustomerId);
            ViewData["StoreID"] = new SelectList(_context.Store, "StoreID", "StoreCity", order.StoreID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Store)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}
