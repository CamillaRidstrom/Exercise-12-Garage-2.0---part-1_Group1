﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise_12_Garage_2._0___part_1_Group1.Data;
using Exercise_12_Garage_2._0___part_1_Group1.Models;
using Exercise_12_Garage_2._0___part_1_Group1.Models.ViewModels;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;

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

            //Search
            string search = string.Empty;
            if (!string.IsNullOrWhiteSpace(vehicle.RegistrationNumber))
            {
                search += vehicle.RegistrationNumber;
                vehicles = vehicles.Where(v => v.RegistrationNumber.StartsWith(vehicle.RegistrationNumber));
            }

            //Sort
            const string RegistrationNumberSort = "RegistrationNumber",
                VehicleTypeSort = "VehicleType",
                ColorSort = "Color",
                ParkingDateSort = "ParkingDate";

            ViewData["RegistrationNumberSort"] = RegistrationNumberSort;
            ViewData["VehicleTypeSort"] = VehicleTypeSort;
            ViewData["ColorSort"] = ColorSort;
            ViewData["ParkingDate"] = ParkingDateSort;

            switch (vehicle.SortOrder)
            {
                case RegistrationNumberSort:
                    vehicles = vehicles.OrderBy(v => v.RegistrationNumber);
                    break;
                case VehicleTypeSort:
                    vehicles = vehicles.OrderBy(s => s.VehicleType);
                    break;
                case ColorSort:
                    vehicles = vehicles.OrderBy(s => s.Color);
                    break;
                case ParkingDateSort:
                    vehicles = vehicles.OrderByDescending(s => s.ParkingDate);
                    break;
                default:
                    break;
            }

            //Display
            vehicle.Vehicles = await vehicles.ToListAsync();
            return View(vehicle);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsRegistrationNumberExists(string registrationNumber, string existingRegistrationNumber)
        {
            if (registrationNumber == existingRegistrationNumber)
            {
                return Json(true);
            }
            bool isRegistrationNumberExists = false;
            try
            {
                isRegistrationNumberExists = await _context.ParkVehicle.AnyAsync(v => v.RegistrationNumber.Equals(registrationNumber));
                return Json(!isRegistrationNumberExists);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        // GET: ParkVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkVehicle == null)
            {
                return NotFound();
            }

            var detailsViewModel = await _context.ParkVehicle
                .Select(v => new DetailViewModel
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Brand = v.Brand,
                    Color = v.Color,
                    Model = v.Model,
                    NumberOfWheels = v.NumberOfWheels,
                    ParkingDate = v.ParkingDate,
                    VehicleType = v.VehicleType
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detailsViewModel == null)
            {
                return NotFound();
            }

            return View(detailsViewModel);
        }

        // GET: ParkVehicles/Park
        public IActionResult Park()
        {
            return View();
        }

        // POST: ParkVehicles/Park
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Id,RegistrationNumber,VehicleType,Color,Brand,Model,NumberOfWheels")] ParkVehicle parkVehicle)
        {
            if (ModelState.IsValid)
            {
                if (await _context.ParkVehicle.AnyAsync(v => v.RegistrationNumber.Equals(parkVehicle.RegistrationNumber)))
                {
                    ModelState.AddModelError("RegistrationNumber", "Registration Number already exists");
                }
                else
                {
                    _context.Add(parkVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }

                _context.Add(parkVehicle);
                await _context.SaveChangesAsync();
                string informationToUser = $"Vehicle {parkVehicle.RegistrationNumber} has been parked";
                TempData["feedback"] = informationToUser;
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

            var editpatkVehicle = new EditParkVehicleViewModel
            {
                Id = parkVehicle.Id,
                ExistingRegistrationNumber = parkVehicle.RegistrationNumber,
                RegistrationNumber = parkVehicle.RegistrationNumber,
                Brand = parkVehicle.Brand,
                Color = parkVehicle.Color,
                Model = parkVehicle.Model,
                NumberOfWheels = parkVehicle.NumberOfWheels,
                ParkingDate = parkVehicle.ParkingDate,
                VehicleType = parkVehicle.VehicleType
            };

            return View(editpatkVehicle);
        }

        // POST: ParkVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditParkVehicleViewModel parkVehicle)
        {
            if (id != parkVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVehicle = await _context.ParkVehicle.FindAsync(parkVehicle.Id);

                    //Checking for changes
                    string propertiesWithAChange = "";
                    if (existingVehicle.VehicleType != parkVehicle.VehicleType)
                    {
                        propertiesWithAChange += "vehicle type and ";
                    }
                    if (existingVehicle.Color != parkVehicle.Color)
                    {
                        propertiesWithAChange += "color and ";
                    }
                    if (existingVehicle.Brand != parkVehicle.Brand)
                    {
                        propertiesWithAChange += "brand and ";
                    }
                    if (existingVehicle.Model != parkVehicle.Model)
                    {
                        propertiesWithAChange += "model and ";
                    }
                    if (existingVehicle.NumberOfWheels != parkVehicle.NumberOfWheels)
                    {
                        propertiesWithAChange += "number of wheels and ";
                    }

                    //Writing changes as feedback
                    if (!string.IsNullOrEmpty(propertiesWithAChange))
                    {
                        propertiesWithAChange = propertiesWithAChange.TrimEnd('a', 'n', 'd', ' ');
                        string informationToUser = $"The <strong>{propertiesWithAChange}</strong> for vehicle <strong>{parkVehicle.RegistrationNumber}</strong> has been updated";
                        TempData["feedback"] = informationToUser;
                    }
                    else
                    {
                        TempData["feedback"] = $"No changes to {parkVehicle.RegistrationNumber} performed";
                    }

                    //Performing changes
                    existingVehicle.RegistrationNumber = parkVehicle.RegistrationNumber;
                    existingVehicle.VehicleType = parkVehicle.VehicleType;
                    existingVehicle.Color = parkVehicle.Color;
                    existingVehicle.Brand = parkVehicle.Brand;
                    existingVehicle.Model = parkVehicle.Model;
                    existingVehicle.NumberOfWheels = parkVehicle.NumberOfWheels;

                    existingVehicle.ParkingDate = existingVehicle.ParkingDate;

                    _context.Update(existingVehicle);
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
                return Problem("Entity set 'Exercise_12_Garage_2_0___part_1_Group1Context.ParkVehicle' is null.");
            }

            var parkVehicle = await _context.ParkVehicle.FindAsync(id);

            if (parkVehicle != null)
            {


                var timePassed = DateTime.Now - parkVehicle.ParkingDate;
                var hoursRoundedDown = (int)Math.Floor(timePassed.TotalHours);
                var minutesRoundedDown = (int)Math.Floor(timePassed.TotalMinutes);

                // Receipt data. Cost is calculated and rounded down. 
                var receiptData = new ReceiptViewModel
                {
                    RegistrationNumber = parkVehicle.RegistrationNumber,
                    Brand = parkVehicle.Brand,
                    Model = parkVehicle.Model,
                    HoursParked = hoursRoundedDown,
                    MinutesParked = minutesRoundedDown,
                    Cost = Math.Floor((hoursRoundedDown * 70) + (minutesRoundedDown * 1.2)),

                };

                _context.ParkVehicle.Remove(parkVehicle);
                await _context.SaveChangesAsync();

                // Pass the receipt data to the view
                string informationToUser = $"{parkVehicle.VehicleType} {parkVehicle.RegistrationNumber} has been collected";
                TempData["feedback"] = informationToUser;
                return View("ReceiptView", receiptData);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ParkVehicleExists(int id)
        {
            return (_context.ParkVehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}