using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ncarlson2749ex1c1.Data;
using ncarlson2749ex1c1.Models;

namespace ncarlson2749ex1c1.Controllers
{
    [Produces("application/json")]
    [Route("api/SubregionsApi")]
    public class SubregionsApiController : Controller
    {
        private readonly WideWorldContext _context;

        public SubregionsApiController(WideWorldContext context)
        {
            _context = context;
        }

        // GET: api/SubregionsApi
        //[HttpGet]
        //public IEnumerable<string> GetSubregions()
        //{
        //    List<string> stringList = new List<string>();
        //    stringList = _context.Countries.Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToList();
        //    return stringList;
        //}
        [HttpGet]
        public IEnumerable<Subregions> GetSubregions()
        {
            List<string> stringList = new List<string>();
            stringList = _context.Countries.Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToList();
            List<Subregions> subregionsList = new List<Subregions>();
            foreach (string s in stringList)
            {
                Subregions sr = new Subregions();
                sr.SubregionName = s;
                subregionsList.Add(sr);
            }
            return subregionsList;
        }

        //private bool CountriesExists(int id)
        //{
        //    return _context.Countries.Any(e => e.CountryId == id);
        //}

        // GET: api/SubregionsApi/Americas
        [HttpGet("{region}")]
        public async Task<IActionResult> GetSubregions([FromRoute] string region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<string> stringList = new List<string>();
            stringList = await _context.Countries.Where(r => r.Region == region).Select(c => c.Subregion).Distinct().OrderBy(r => 1).ToListAsync();
            List<Subregions> subregionsList = new List<Subregions>();
            foreach (string s in stringList)
            {
                Subregions sr = new Subregions();
                sr.SubregionName = s;
                subregionsList.Add(sr);
            }

            if (subregionsList == null)
            {
                return NotFound();
            }

            return Ok(subregionsList);
        }
    }
}

