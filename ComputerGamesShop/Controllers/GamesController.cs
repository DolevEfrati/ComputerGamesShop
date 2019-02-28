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
    public enum MULTIPLAYER_OPTIONS
    {
        yes,
        no
    };

    public class GamesController : Controller
    {
        private readonly ComputerGamesShopContext _context;

        public GamesController(ComputerGamesShopContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var computerGamesShopContext = _context.Game.Include(g => g.Publisher);
            ViewBag.MaxPrice = _context.Game.Select(x => x.Price).Max();
            return View(await computerGamesShopContext.ToListAsync());
        }

        // POST Games/Filter
        [HttpPost]
        public async Task<IActionResult> Filter(GameQuery query)
        {
            bool isMulti = query.Type.Equals("Yes");
            ViewBag.CurrentPrice = query.Price;
            ViewBag.MaxPrice = _context.Game.Select(x => x.Price).Max();

            var computerGamesShopContext = _context.Game.Where((Game game) =>
                game.Price <= query.Price &&
                (query.Text == null || game.Title.Contains(query.Text)) &&
                (query.Type.Equals("Both") || game.IsMultiplayer == isMulti))
                .Include(g => g.Publisher);

            return View("index", await computerGamesShopContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Publisher)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Genre,Price,Image,PublisherID,IsMultiplayer,ReleaseDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "Name", game.PublisherID);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.SingleOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "Name", game.PublisherID);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Genre,Price,Image,PublisherID,IsMultiplayer,ReleaseDate")] Game game)
        {
            if (id != game.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.ID))
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
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "Name", game.PublisherID);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Publisher)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.SingleOrDefaultAsync(m => m.ID == id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.ID == id);
        }
    }
}
