using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatinApp.API.Data;
using DatinApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dataing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.ValuesSet.ToListAsync();
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetValue(int id)
        {
            var value = await _context.ValuesSet.FirstOrDefaultAsync(o => o.Id == id);
            return Ok(value);
        }
    }
}
