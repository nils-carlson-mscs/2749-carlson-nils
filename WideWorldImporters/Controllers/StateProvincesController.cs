using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WideWorldImporters.Data;
using WideWorldImporters.Models;

namespace WideWorldImporters.Controllers
{
    public class StateProvincesController : Controller
    {
        private readonly WideWorldContext _context;

        public StateProvincesController(WideWorldContext context)
        {
            _context = context;
        }

        // GET: StateProvinces
        public async Task<IActionResult> Index()
        {
            var wideWorldContext = _context.StateProvinces.Include(s => s.Country);
            return View(await wideWorldContext.ToListAsync());
        }

        // GET: StateProvinces/Details/5
        public async Task<IActionResult> Details(int? id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PopulationSortParm"] = sortOrder == "pop_asc" ? "pop_desc" : "pop_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var stateProvinces = await _context.StateProvinces
                .Include(s => s.Country)
                //.Include(s => s.Cities)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.StateProvinceId == id);
            var cities = _context.Cities
                .Where(c => c.StateProvinceId == id);

            //cities = cities.OrderByDescending(c => c.LatestRecordedPopulation);

            if (stateProvinces == null)
            {
                return NotFound();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                cities = cities.Where(c => c.CityName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    cities = cities.OrderByDescending(c => c.CityName);
                    break;
                case "pop_asc":
                    cities = cities.OrderBy(c => c.LatestRecordedPopulation);
                    break;
                case "pop_desc":
                    cities = cities.OrderByDescending(c => c.LatestRecordedPopulation);
                    break;
                default:
                    cities = cities.OrderBy(c => c.CityName);
                    break;
            }

            //ViewData["Cities"] = await cities.AsNoTracking().ToListAsync();
            int pageSize = 8;
            ViewData["Cities"] = await PaginatedList<Cities>.CreateAsync(cities.AsNoTracking(), page ?? 1, pageSize);
            return View(stateProvinces);
        }

        // GET: StateProvinces/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.CountryId = id.ToString();

            //ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "Continent");
            return View();
        }

        // POST: StateProvinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateProvinceCode,StateProvinceName,CountryId,SalesTerritory,LatestRecordedPopulation")] StateProvinces stateProvince)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stateProvince);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Countries", new { id = stateProvince.CountryId.ToString() });
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
            ViewBag.CountryId = stateProvince.CountryId.ToString();
            return View(stateProvince);
        }

        // GET: StateProvinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateProvince = await _context.StateProvinces.Include("Country").SingleOrDefaultAsync(m => m.StateProvinceId == id);
            if (stateProvince == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries.Where(s => s.Subregion == stateProvince.Country.Subregion), "CountryId", "CountryName", stateProvince.CountryId);
            return View(stateProvince);
        }

        // POST: StateProvinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StateProvinces stateProvince = await _context.StateProvinces.Include("Country").SingleOrDefaultAsync(s => s.StateProvinceId == id);
            stateProvince.LastEditedBy = 1;
            if (await TryUpdateModelAsync<StateProvinces>(
                stateProvince, "",
                s => s.StateProvinceCode, s => s.StateProvinceName, 
                s => s.CountryId, s => s.SalesTerritory, 
                s => s.LatestRecordedPopulation, s => s.LastEditedBy))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Countries", new { id = stateProvince.CountryId.ToString() });
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewData["CountryId"] = new SelectList(_context.Countries.Where(s => s.Subregion == stateProvince.Country.Subregion), "CountryId", "CountryName", stateProvince.CountryId);
            return View(stateProvince);
        }

        // GET: StateProvinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stateProvinces = await _context.StateProvinces
                .Include(s => s.Country)
                .SingleOrDefaultAsync(m => m.StateProvinceId == id);
            if (stateProvinces == null)
            {
                return NotFound();
            }

            return View(stateProvinces);
        }

        // POST: StateProvinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stateProvinces = await _context.StateProvinces.SingleOrDefaultAsync(m => m.StateProvinceId == id);
            _context.StateProvinces.Remove(stateProvinces);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Details", "Countries", new { id = stateProvinces.CountryId.ToString() });
        }

        private bool StateProvincesExists(int id)
        {
            return _context.StateProvinces.Any(e => e.StateProvinceId == id);
        }
    }
}
