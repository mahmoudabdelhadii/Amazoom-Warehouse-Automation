using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Amazoom.Data;
using Microsoft.AspNetCore.Authorization;
using Manager.Authorization;

namespace Amazoom.Models
{
    [Authorize(Policy = "admin")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        [Authorize(Policy = "admin")]
        public IActionResult Shutdown()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Warehouse.ToListAsync());
        }

        [Authorize(Policy = "admin")]
        public async Task<IActionResult> ItemStock()
        {
            return View(await _context.Item.ToListAsync());
        }

        // GET: Warehouse/Details/5
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouse
                .FirstOrDefaultAsync(m => m.ID == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(await _context.Item.Where(s => s.WarehouseStored == id).ToListAsync());
        }

        // GET: Warehouse/Create
        [Authorize(Policy = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ColumnNumber,RowNumber,ShelfSize,ShelfMaxWeight")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouse);
                await _context.SaveChangesAsync();
                for (int i = 0; i < 2; i++)
                {
                    Truck newtruck = new Truck
                    {
                        WarehouseStored = warehouse.ID,
                        total_weight = 50,
                        Docked = true
                    };
                    _context.Add(newtruck);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }
    }
}
