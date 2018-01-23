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
    public class PeopleController : Controller
    {
        private readonly WideWorldContext _context;

        public PeopleController(WideWorldContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(string EmailDomain)
        {
            if (EmailDomain == null)
                EmailDomain = "wideworldimporters.com";
            ViewBag.EmailDomainSelectList =
                new SelectList(await _context.People
                .Where(p => p.EMailAddress != null)
                .Select(p => p.EMailAddress.Substring(p.EMailAddress.IndexOf("@") + 1))
                .Distinct()
                .OrderBy(r => 1)
                .ToListAsync(), "", "", "");
            return View(await _context.People
                .Include(p => p.CustomersPrimaryContact)
                .Include(p => p.CustomersAlternateContact)
                .Where(p => p.EMailAddress.Contains(EmailDomain))
                .OrderBy(p => p.FullName)
                .ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .SingleOrDefaultAsync(m => m.PersonId == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,PreferredName,IsPermittedToLogon,LogonName,IsExternalLogonProvider,HashedPassword,IsSystemUser,IsEmployee,IsSalesperson,UserPreferences,PhoneNumber,FaxNumber,EMailAddress,Photo,CustomFields,LastEditedBy,ValidFrom,ValidTo")] People people)
        {
            if (ModelState.IsValid)
            {
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(people);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People.SingleOrDefaultAsync(m => m.PersonId == id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FullName,PreferredName,IsPermittedToLogon,LogonName,IsExternalLogonProvider,HashedPassword,IsSystemUser,IsEmployee,IsSalesperson,UserPreferences,PhoneNumber,FaxNumber,EMailAddress,Photo,CustomFields,LastEditedBy,ValidFrom,ValidTo")] People people)
        {
            if (id != people.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.PersonId))
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
            return View(people);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .SingleOrDefaultAsync(m => m.PersonId == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var people = await _context.People.SingleOrDefaultAsync(m => m.PersonId == id);
            _context.People.Remove(people);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.PersonId == id);
        }
    }
}
