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
    public class CustomersController : Controller
    {
        private readonly WideWorldContext _context;

        public CustomersController(WideWorldContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(int? id)
        {
            var customers = _context.Customers
                .Include(c => c.CustomerCategory)
                .Include(c => c.DeliveryCity)
                    .ThenInclude(city => city.StateProvince)
                    .ThenInclude(s => s.Country)
                .Include(c => c.PrimaryContactPerson)
                .OrderBy(c => c.DeliveryCity.StateProvince.Country.CountryName)
                    .ThenBy(c => c.DeliveryCity.StateProvince.StateProvinceCode)
                    .ThenBy(c => c.DeliveryCity.CityName)
                    .ThenBy(c => c.CustomerName);

            List<Customers> custList = null;
            if (id.HasValue)
            {
                custList = await customers.Where(c => c.DeliveryCity.StateProvinceId == id.Value).ToListAsync();
                ViewData["CountryId"] = custList.First().DeliveryCity.StateProvince.CountryId;
            }
            else
            {
                ViewData["CountryId"] = "";
                custList = await customers.ToListAsync();
            }
            return View(custList);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .Include(c => c.AlternateContactPerson)
                .Include(c => c.CustomerCategory)
                .Include(c => c.DeliveryCity)
                    .ThenInclude(c => c.StateProvince)
                    .ThenInclude(s => s.Country)
                .Include(c => c.PostalCity)
                    .ThenInclude(c => c.StateProvince)
                    .ThenInclude(s => s.Country)
                .Include(c => c.PrimaryContactPerson)
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["AlternateContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName");
            ViewData["CustomerCategoryId"] = new SelectList(_context.CustomerCategories, "CustomerCategoryId", "CustomerCategoryName");
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["PostalCityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["PrimaryContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,BillToCustomerId,CustomerCategoryId,BuyingGroupId,PrimaryContactPersonId,AlternateContactPersonId,DeliveryMethodId,DeliveryCityId,PostalCityId,CreditLimit,AccountOpenedDate,StandardDiscountPercentage,IsStatementSent,IsOnCreditHold,PaymentDays,PhoneNumber,FaxNumber,DeliveryRun,RunPosition,WebsiteURL,DeliveryAddressLine1,DeliveryAddressLine2,DeliveryPostalCode,DeliveryLocation,PostalAddressLine1,PostalAddressLine2,PostalPostalCode,LastEditedBy,ValidFrom,ValidTo")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlternateContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName", customers.AlternateContactPersonId);
            ViewData["CustomerCategoryId"] = new SelectList(_context.CustomerCategories, "CustomerCategoryId", "CustomerCategoryName", customers.CustomerCategoryId);
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "CityId", "CityName", customers.DeliveryCityId);
            ViewData["PostalCityId"] = new SelectList(_context.Cities, "CityId", "CityName", customers.PostalCityId);
            ViewData["PrimaryContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName", customers.PrimaryContactPersonId);
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            string webDomain = customer.WebsiteURL;
            if (webDomain.StartsWith("http://www")) webDomain = webDomain.Substring(11);
            if (webDomain.StartsWith("https://www")) webDomain = webDomain.Substring(12);
            if (webDomain.StartsWith("www")) webDomain = webDomain.Substring(4);
            if (webDomain.IndexOf("/") > -1) webDomain = webDomain.Substring(0, webDomain.IndexOf("/"));
            List<People> companyContacts = await _context.People
                .Where(p => p.EMailAddress.Contains(webDomain))
                .OrderBy(p => p.FullName)
                .ToListAsync();
            ViewData["PrimaryContactPersonId"] = new SelectList(companyContacts, "PersonId", "FullName", customer.PrimaryContactPersonId);
            ViewData["AlternateContactPersonId"] = new SelectList(companyContacts, "PersonId", "FullName", customer.AlternateContactPersonId);
            ViewData["CustomerCategoryId"] = new SelectList(_context.CustomerCategories, "CustomerCategoryId", "CustomerCategoryName", customer.CustomerCategoryId);
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "CityId", "CityName", customer.DeliveryCityId);
            ViewData["PostalCityId"] = new SelectList(_context.Cities, "CityId", "CityName", customer.PostalCityId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,BillToCustomerId,CustomerCategoryId,BuyingGroupId,PrimaryContactPersonId,AlternateContactPersonId,DeliveryMethodId,DeliveryCityId,PostalCityId,CreditLimit,AccountOpenedDate,StandardDiscountPercentage,IsStatementSent,IsOnCreditHold,PaymentDays,PhoneNumber,FaxNumber,DeliveryRun,RunPosition,WebsiteURL,DeliveryAddressLine1,DeliveryAddressLine2,DeliveryPostalCode,DeliveryLocation,PostalAddressLine1,PostalAddressLine2,PostalPostalCode,LastEditedBy,ValidFrom,ValidTo")] Customers customers)
        {
            if (id != customers.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.CustomerId))
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
            ViewData["AlternateContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName", customers.AlternateContactPersonId);
            ViewData["CustomerCategoryId"] = new SelectList(_context.CustomerCategories, "CustomerCategoryId", "CustomerCategoryName", customers.CustomerCategoryId);
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "CityId", "CityName", customers.DeliveryCityId);
            ViewData["PostalCityId"] = new SelectList(_context.Cities, "CityId", "CityName", customers.PostalCityId);
            ViewData["PrimaryContactPersonId"] = new SelectList(_context.People, "PersonId", "FullName", customers.PrimaryContactPersonId);
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .Include(c => c.AlternateContactPerson)
                .Include(c => c.CustomerCategory)
                .Include(c => c.DeliveryCity)
                .Include(c => c.PostalCity)
                .Include(c => c.PrimaryContactPerson)
                .SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerId == id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
