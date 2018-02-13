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
    [Route("api/RegionsApi")]
    public class RegionsApiController : Controller
    {
        private readonly WideWorldContext _context;

        public RegionsApiController(WideWorldContext context)
        {
            _context = context;
        }

        // GET: api/RegionsApi
        //[HttpGet]
        //public IEnumerable<string> GetRegions()
        //{
        //    List<string> stringList = new List<string>();
        //    stringList = _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToList();
        //    return stringList;
        //}
        [HttpGet]
        public IEnumerable<Regions> GetRegions()
        {
            List<string> stringList = new List<string>();
            stringList = _context.Countries.Select(c => c.Region).Distinct().OrderBy(r => 1).ToList();
            List<Regions> regionsList = new List<Regions>();
            foreach(string s in stringList)
            {
                Regions r = new Regions();
                r.RegionName = s;
                regionsList.Add(r);
            }
            return regionsList;
        }

        //private bool CountriesExists(int id)
        //{
        //    return _context.Countries.Any(e => e.CountryId == id);
        //}
    }
}
