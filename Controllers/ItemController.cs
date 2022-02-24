using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Amazoom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace Amazoom.Models
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Item
        public async Task<IActionResult> Index()
        {

            return View(await _context.Item.Where(s => s.InCart == false).ToListAsync());
        }

        // GET: Item
        public async Task<IActionResult> CartAdd(int? id)
        {

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            item.InCart = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Item
        public async Task<IActionResult> CartRemove(int? id)
        {

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            item.InCart = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(int id)
        {
            Item item = _context.Item.Find(id);
            Warehouse warehouse = _context.Warehouse.Find(item.WarehouseStored);
            Truck truck = _context.Truck.Where(s => s.Docked == true).Where(a => a.WarehouseStored == item.WarehouseStored).Where(a => (item.Weight + a.current_weight) < a.total_weight).FirstOrDefault();
            Truck truck1 = _context.Truck.Where(s => s.Docked == false).FirstOrDefault();
            if (truck == null)
            {
                return RedirectToAction("Error", "Shared", new { area = "" });
            }
            Console.WriteLine("{0} will be put in Truck {1} from Warehouse {2}", item.Name, truck.ID, truck.WarehouseStored);
            truck.current_weight = truck.current_weight + item.Weight;
            Console.WriteLine("Truck {0} weight: {1} ", truck.ID, truck.current_weight);
            Warehousecomp.Pickupitem(item, warehouse, truck);
            if (truck.current_weight >= 0.75 * truck.total_weight)
            {
                Console.WriteLine("Truck {0} full, leaving bay when robot delivers ", truck.ID);
                truck.Docked = false;
                truck.current_weight = 0;
            }
            _context.Item.Remove(item);
            if (truck1 == null)
            {
            }
            else
            {
                Console.WriteLine("Truck {0} has returned from delivery", truck1.ID);
                truck1.Docked = true;
            }
           _context.SaveChanges();

           return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> Cart()
        {
            return View(await _context.Item.Where(s => s.InCart == true).ToListAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        [Authorize(Policy = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Create([Bind("ID,Name,Weight,WarehouseStored")] Item item,string Quantity)
        {
            if (ModelState.IsValid)
            {
                int QuantityInt = Convert.ToInt32(Quantity);
                var warehousetoplace = _context.Warehouse
                                           .Where(s => s.ID == item.WarehouseStored)
                                           .FirstOrDefault();
                for (int i = 0;i < QuantityInt; i++)
                {
                 Item newitem = item;
                 newitem.ID = item.ID + i;
                warehousetoplace.PlaceItem(newitem);
                _context.Add(newitem);
                await _context.SaveChangesAsync();
                }
                return RedirectToAction("ItemStock", "Warehouse", new { area = "" });
            }
            return View(item);
        }

        // GET: Item/Edit/5
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Weight,Column,Row,Side,Shelf,WarehouseStored")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
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
            return View(item);
        }

        // GET: Item/Delete/5
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ItemStock", "Warehouse", new { area = "" });
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ID == id);
        }
        public string GetCartId()
        {            
            return User.Identity.Name;
        }
    }
}
