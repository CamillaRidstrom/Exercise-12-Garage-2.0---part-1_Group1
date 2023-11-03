using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise_12_Garage_2._0___part_1_Group1.Data;
using Exercise_12_Garage_2._0___part_1_Group1.Models;
using Exercise_12_Garage_2._0___part_1_Group1.Models.ViewModels;

namespace Exercise_12_Garage_2._0___part_1_Group1.Controllers
{
    public class ParkVehiclesController : Controller
    {
        private readonly Exercise_12_Garage_2_0___part_1_Group1Context _context;

        public ParkVehiclesController(Exercise_12_Garage_2_0___part_1_Group1Context context)
        {
            _context = context;
        }

        // GET: ParkVehicles
        public async Task<IActionResult> Index()
        {
              return _context.ParkVehicle != null ? 
                          View(await _context.ParkVehicle.ToListAsync()) :
                          Problem("Entity set 'Exercise_12_Garage_2_0___part_1_Group1Context.ParkVehicle'  is null.");
        }

		public async Task<IActionResult> Search(SearchParkVehicleViewModel vehicle)
		{
            var vehicles = _context.ParkVehicle.AsQueryable();

            if (!string.IsNullOrWhiteSpace(vehicle.RegistrationNumber))
            {
                vehicles = vehicles.Where(v => v.RegistrationNumber.StartsWith(vehicle.RegistrationNumber));
            }


            vehicle.Vehicles = await vehicles.ToListAsync();

            return View(vehicle);
		}

		// GET: ParkVehicles/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkVehicle == null)
            {
                return NotFound();
            }

            return View(parkVehicle);
        }

        // GET: ParkVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegistrationNumber,VehicleType,Color,Brand,Model,NumberOfWheels")] ParkVehicle parkVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkVehicle);
        }

        // GET: ParkVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle.FindAsync(id);
            if (parkVehicle == null)
            {
                return NotFound();
            }
            return View(parkVehicle);
        }

        // POST: ParkVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationNumber,VehicleType,Color,Brand,Model,NumberOfWheels")] ParkVehicle parkVehicle)
        {
            if (id != parkVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkVehicleExists(parkVehicle.Id))
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
            return View(parkVehicle);
        }

        // GET: ParkVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }

            var parkVehicle = await _context.ParkVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkVehicle == null)
            {
                return NotFound();
            }

            return View(parkVehicle);
        }

        // POST: ParkVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkVehicle == null)
            {
                return Problem("Entity set 'Exercise_12_Garage_2_0___part_1_Group1Context.ParkVehicle'  is null.");
            }
            var parkVehicle = await _context.ParkVehicle.FindAsync(id);
            if (parkVehicle != null)
            {
                _context.ParkVehicle.Remove(parkVehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkVehicleExists(int id)
        {
          return (_context.ParkVehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
