using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerGamesShop.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ComputerGamesShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComputerGamesShopContext _context;

        public HomeController(ComputerGamesShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/login
        public IActionResult Login()
        {
            return File("~/login.html", "text/html");
        }

        // POST: Home/login
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password")] User loginData)
        {
            if (loginData.Email == null || loginData.Password == null)
            {
                return Unauthorized();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.Email == loginData.Email &&
                                        m.Password == loginData.Password);
            if (user == null)
                return Unauthorized();
            HttpContext.Session.SetString(Globals.USER_SESSION_KEY, JsonConvert.SerializeObject(user));
            HttpContext.Session.SetString(Globals.IS_MANAGER_SESSION_KEY, (user.Role == Role.Manager).ToString().ToLower());
            Globals.setConnectedUser(HttpContext.Session);

            List<Store> stores = await _context.Store.ToListAsync();
            Globals.setStores(stores);
            return Ok();
        }

        // GET: Home/logout
        public IActionResult Logout()
        {
            HttpContext.Session.SetString(Globals.IS_MANAGER_SESSION_KEY, "false");
            HttpContext.Session.Remove(Globals.USER_SESSION_KEY);
            Globals.clearCart();
            Globals.removeUser(HttpContext.Session);

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Statistics()
        {
            var orderPerStore = await _context.Order
                .GroupBy(p => p.Store.DisplayName)
                .Select(x => new { name = x.Key, count = x.Count() })
                .ToListAsync();

            var gamesPerPublisher = await _context.Game
                .GroupBy(p => p.Publisher.Name)
                .Select(x => new { name = x.Key, count = x.Count() })
                .ToListAsync();

            var results = new
            {
                items = new[] { orderPerStore, gamesPerPublisher }
            };

            return Json(results);
        }

        // GET: /statistic
        [Route("statistics")]
        public IActionResult getViewStatistics()
        {
            return View("Statistics");
        }

        // GET: /statistic
        [Route("recommendation")]
        public IActionResult getViewRecommendation()
        {
            return View("Recommendation");
        }
    }
}
