using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WideWorldImporters.Data;
using WideWorldImporters.Models;

using WideWorldImporters.Models.StatisticsViewModels;

namespace WideWorldImporters.Controllers
{
    public class CountriesController : Controller
    {
        private readonly WideWorldContext _context;

        public CountriesController(WideWorldContext context)
        {
            _context = context;
        }

        // GET: Countries
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Index()
        {
            string selectedRegion = "Americas";
            string selectedSubregion = "Northern America";

            try
            {
                selectedRegion = HttpContext.Request.Form["selectRegion"];
                selectedSubregion = HttpContext.Request.Form["selectSubregion"];
            }
            catch (InvalidOperationException ex) { }

            List<string> regionList = new List<string>();
            regionList = await _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToListAsync();
            //ViewBag.RegionList = regionList;
            List<SelectListItem> regionSelectListItems = new List<SelectListItem>();
            foreach (string r in regionList)
            {
                regionSelectListItems.Add(
                    new SelectListItem { Text = r, Value = r, Selected = (r == selectedRegion) });
            }
            ViewBag.RegionSelectListItems = regionSelectListItems;

            List<string> subregionList = new List<string>();
            subregionList = await _context.Countries.Where(r => r.Region == selectedRegion).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync();
            //ViewBag.SubregionList = subregionList;
            List<SelectListItem> subregionSelectListItems = new List<SelectListItem>();
            foreach (string r in subregionList)
            {
                subregionSelectListItems.Add(
                    new SelectListItem() { Value = r, Text = r, Selected = (r == selectedSubregion) });
            }
            ViewBag.SubregionSelectListItems = subregionSelectListItems;
            if (subregionSelectListItems.Where(s => s.Selected).SingleOrDefault() == null)
            {
                subregionSelectListItems[0].Selected = true;
                selectedSubregion = subregionSelectListItems[0].Value;
            }
            return View(await _context.Countries.Where(c => c.Subregion == selectedSubregion).ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries
                .Include(c => c.StateProvinces)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CountryId == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }

        // GET: Countries/Create
        //public IActionResult Create()
        public async Task<IActionResult> Create()       // Change signature to "async Task<>"
        {
            string selectedRegion = "Americas";
            string selectedSubregion = "Northern America";
            ViewBag.PreviousRegion = selectedRegion;
            ViewBag.PreviousSubregion = selectedSubregion;
            ViewBag.RegionSelectList =
                new SelectList(await _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToListAsync(), "", "", selectedRegion);
            ViewBag.SubregionSelectList =
                new SelectList(await _context.Countries.Where(r => r.Region == selectedRegion).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync(), "", "", selectedSubregion);
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryName,FormalName,IsoAlpha3Code,IsoNumericCode,CountryType,LatestRecordedPopulation,Continent,Region,Subregion")] Countries country)
        {
            if (ModelState.IsValid)
            {
                string previousRegion = HttpContext.Request.Form["previousRegion"];
                if (previousRegion != country.Region)
                {
                    SelectList regionSelectList =
                        new SelectList(await _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToListAsync(), "", "", country.Region);
                    ViewBag.RegionSelectList = regionSelectList;
                    ViewBag.PreviousRegion = country.Region;
                    ViewBag.SubregionSelectList =
                        new SelectList(await _context.Countries.Where(r => r.Region == country.Region).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync());
                }
                else
                {
                    country.LastEditedBy = 1;
                    _context.Add(country);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }
                }
            }
            return View(country);
        }

        // After "if (ModelState.IsValid), add:
        // Get previousRegion from view
        // If country.Region doesn't match previousRegion
        // Load regionSelectList:
        //    SelectList regionSelectList =
        //        new SelectList(await _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToListAsync(), "", "", country.Region);
        // Add RegionSelectList to ViewBag
        // Set ViewBag.PreviousRegion = country.Region
        // Add SubregionSelectList to ViewBag:
        //     ViewBag.SubregionSelectList =
        //         new SelectList(await _context.Countries.Where(r => r.Region == country.Region).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync());

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries.SingleOrDefaultAsync(m => m.CountryId == id);
            if (countries == null)
            {
                return NotFound();
            }

            string selectedRegion = countries.Region;
            string selectedSubregion = countries.Subregion;
            List<string> subregionList = new List<string>();
            subregionList = await _context.Countries.Where(r => r.Region == selectedRegion).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync();
            List<SelectListItem> subregionSelectListItems = new List<SelectListItem>();
            foreach (string r in subregionList)
            {
                subregionSelectListItems.Add(
                    new SelectListItem() { Value = r, Text = r, Selected = (r == selectedSubregion) });
            }
            ViewBag.SubregionSelectListItems = subregionSelectListItems;

            return View(countries);
        }

        // POST: Countries/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var countryToUpdate = await _context.Countries.SingleOrDefaultAsync(c => c.CountryId == id);
            if (await TryUpdateModelAsync<Countries>(
                countryToUpdate,
                "",
                c => c.CountryName, c => c.FormalName, c => c.LatestRecordedPopulation, c => c.Subregion))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(countryToUpdate);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries
                .SingleOrDefaultAsync(m => m.CountryId == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var countries = await _context.Countries.SingleOrDefaultAsync(m => m.CountryId == id);
            _context.Countries.Remove(countries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountriesExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }

        public async Task<IActionResult> SalesTStats(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.Countries
                .Include(c => c.StateProvinces)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CountryId == id);
            if (countries == null)
            {
                return NotFound();
            }

            ViewData["CountryName"] = countries.CountryName;

            IQueryable<SalesTerritoryGroup> groupedQry =
                from stateProvince in _context.StateProvinces
                where stateProvince.CountryId == id
                group stateProvince by stateProvince.SalesTerritory into territoryGroup
                select new SalesTerritoryGroup()
                {
                    SalesTerritory = territoryGroup.Key,
                    TerritoryStateCount = territoryGroup.Count(),
                    TerritorySumPopulation = territoryGroup.Sum(g => g.LatestRecordedPopulation)
                };
            return View(await groupedQry.AsNoTracking().ToListAsync());
        }
    }
}
