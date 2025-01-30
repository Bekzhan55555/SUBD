using Microsoft.AspNetCore.Mvc;
using SUBD.Models;
using SUBD.Context;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SUBD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AddDBContext _context;

        public HomeController(ILogger<HomeController> logger, AddDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult TestConnection()
        {
            try
            {
                var canConnect = _context.Database.CanConnect();
                if (canConnect)
                    return Content("Connection to database is successful.");
                else
                    return Content("Unable to connect to database.");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message}");
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Units(int id = 0)
        {
            // Если id == 0, берем первую запись
            if (id == 0)
            {
                id = await _context.Units.MinAsync(u => u.Id);
            }

            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            // Определяем границы
            ViewBag.FirstId = await _context.Units.MinAsync(u => u.Id);
            ViewBag.LastId = await _context.Units.MaxAsync(u => u.Id);

            // Определяем предыдущий и следующий ID
            ViewBag.PrevId = await _context.Units
                .Where(u => u.Id < id)
                .OrderByDescending(u => u.Id)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            ViewBag.NextId = await _context.Units
                .Where(u => u.Id > id)
                .OrderBy(u => u.Id)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return View(unit);
        }


        public async Task<IActionResult> Units_Edit(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            // Указываем, что используем файл "Units_Edit.cshtml"
            return View("Units_Edit", unit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Units unit)
        {
            if (ModelState.IsValid)
            {
                _context.Update(unit);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Запись успешно обновлена!";
                return RedirectToAction(nameof(Units), new { id = unit.Id });
            }
            return View(unit);
        }

        public async Task<IActionResult> Units_Delete(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Запись успешно удалена!";
            }
            return RedirectToAction(nameof(Units));
        }

        public IActionResult Units_Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Units unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Запись успешно добавлена!";
                return RedirectToAction(nameof(Units), new { id = unit.Id });
            }
            return View(unit);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
