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
    public class OrderItemsController : Controller
    {
        private readonly ComputerGamesShopContext _context;

        public OrderItemsController(ComputerGamesShopContext context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var computerGamesShopContext = _context.Game.Where(p => Globals.getCartList().Contains(p.ID));
            ViewBag.CurrentOrderId = getCurrentOrderId();
            return View(await computerGamesShopContext.ToListAsync());
        }

        public int getCurrentOrderId()
        {
            var MaxOrderID = 0;
            try
            {
                MaxOrderID = _context.Order.Select(x => x.OrderID).Max();
            }
            catch // order table is empty
            {
            }
            return (MaxOrderID + 1);
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems
                .Include(o => o.Game)
                .Include(o => o.Order)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (orderItems == null)
            {
                return NotFound();
            }

            return View(orderItems);
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["gameId"] = new SelectList(_context.Game, "ID", "Description");
            ViewData["orderId"] = new SelectList(_context.Order, "OrderID", "OrderID");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,orderId,gameId")] OrderItems orderItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["gameId"] = new SelectList(_context.Game, "ID", "Description", orderItems.gameId);
            ViewData["orderId"] = new SelectList(_context.Order, "OrderID", "OrderID", orderItems.orderId);
            return View(orderItems);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems.SingleOrDefaultAsync(m => m.ID == id);
            if (orderItems == null)
            {
                return NotFound();
            }
            ViewData["gameId"] = new SelectList(_context.Game, "ID", "Description", orderItems.gameId);
            ViewData["orderId"] = new SelectList(_context.Order, "OrderID", "OrderID", orderItems.orderId);
            return View(orderItems);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,orderId,gameId")] OrderItems orderItems)
        {
            if (id != orderItems.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemsExists(orderItems.ID))
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
            ViewData["gameId"] = new SelectList(_context.Game, "ID", "Description", orderItems.gameId);
            ViewData["orderId"] = new SelectList(_context.Order, "OrderID", "OrderID", orderItems.orderId);
            return View(orderItems);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItems = await _context.OrderItems
                .Include(o => o.Game)
                .Include(o => o.Order)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (orderItems == null)
            {
                return NotFound();
            }

            return View(orderItems);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderItems = await _context.OrderItems.SingleOrDefaultAsync(m => m.ID == id);
            _context.OrderItems.Remove(orderItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemsExists(int id)
        {
            return _context.OrderItems.Any(e => e.ID == id);
        }

        [HttpPost("/api/addToCart")]
        public async Task<IActionResult> AddToCart(int gameId)
        {
            Globals.addToCart(gameId);
            return Ok();
        }

        [HttpPost("/api/deletefromCart")]
        public async Task<IActionResult> DeletefromCart(int gameId)
        {
            Globals.deleteFromCart(gameId);
            return Ok();
        }
        
        [HttpPost("/api/saveOrder")]
        public IActionResult SaveOrder(int storeId)
        {
            var currentId = getCurrentOrderId();
            // save order
            Order currentOrderData = new Order
            {
                CustomerId = Globals.getConnectedUser()["UserID"].ToObject<int>(),
                StoreID = storeId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItems>()
            };
            
            // save order's items
            List<OrderItems> orderItemsToSave = new List<OrderItems>();

            foreach (int gameId in Globals.getCartList())
            {
                currentOrderData.OrderItems.Add(new OrderItems { orderId = currentId, gameId = gameId});
            }
            _context.Order.Add(currentOrderData);
            _context.SaveChanges();

            // update order id
            ViewBag.CurrentOrderId = ViewBag.CurrentOrderId + 1;

            // clean cart
            Globals.clearCart();

            return Ok();
        }
    }
}
