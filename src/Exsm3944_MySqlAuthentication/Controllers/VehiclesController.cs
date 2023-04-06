using Exsm3944_MySqlAuthentication.Data;
using Exsm3944_MySqlAuthentication.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Exsm3944_MySqlAuthentication.Controllers
{
    public class VehiclesController : Controller
    {

        /// <summary>
        /// Loads the summary view pages
        /// </summary>
        /// <returns>The view with the list of current users vehicles</returns>
        public IActionResult Summary()
        {
            if(User.Identity.Name != null)
            {
                ViewBag.Email = User.Identity.Name;
                return View(VehicleHandler.GetVehicleByEmail(User.Identity.Name));
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Loads the detials view page
        /// </summary>
        /// <param name="vehicleId">The id of vehicle being viewed</param>
        /// <returns>The view page for details with the specified vehicle object</returns>
        public IActionResult Details(int vehicleId)
        {
            if(User.Identity.Name != null)
            {
                if(vehicleId != null)
                {
                    return View(VehicleHandler.GetVehicle(vehicleId));
                }
                return RedirectToAction("Summary", "Vehicles");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The create view page
        /// </summary>
        /// <returns>The view for the create page with the basic vehicle object</returns>
        public IActionResult Create()
        {
            if(User.Identity.Name != null)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.ID = 0;
                vehicle.CustomerEmail= User.Identity.Name;
                return View(vehicle);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The postback for the create page
        /// </summary>
        /// <param name="vehicle">The vehicle object being created</param>
        /// <returns>If no errors then to the summary, else back to the create page with the old vehicle</returns>
        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            const string pattern = @"[a-zA-Z\d-_]+$";
            Regex rg = new Regex(pattern);
            bool proceed = false;
            if(vehicle.Manufacturer != null)
            {
                if(rg.Match(vehicle.Manufacturer).Success)
                {
                    proceed = true;
                }
                else
                {
                    ModelState.AddModelError("Manufacturer", "Only Numbers, letters, Underscores, and Spaces in Manufacturer");
                }
            }

            if(vehicle.VIN != null)
            {
                foreach(char c in vehicle.VIN)
                {
                    if(!char.IsUpper(c) && !char.IsDigit(c))
                    {
                        ModelState.AddModelError("VIN", "VIN can only contain Numbers and Uppercases.");
                        break;
                    }
                }
            }
            
            if(ModelState.IsValid && proceed)
            { //checking model state

                VehicleHandler.CreateVehicle(vehicle.CustomerEmail, vehicle.VIN, vehicle.ModelYear, vehicle.Manufacturer, vehicle.Colour, vehicle.Model, vehicle.PurchaseDate, vehicle.SaleDate);
                return RedirectToAction("Summary");
                              
            }

            return View(vehicle);
        }

        /// <summary>
        /// The view for the edit page.
        /// </summary>
        /// <param name="vehicleId">The id of the vehicle being edited</param>
        /// <returns>The view for the edit page with the specified vehicle object</returns>
        public IActionResult Edit(int vehicleId)
        {
            if(User.Identity.Name != null)
            {
                if(vehicleId != null)
                {
                    return View(VehicleHandler.GetVehicle(vehicleId));
                }

                return RedirectToAction("Summary", "Vehicles");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The postback for the edit page
        /// </summary>
        /// <param name="vehicle">The vehicle object being edited</param>
        /// <returns>To the summary if no errors, else back to the edit page with the old vehicle object</returns>
        [HttpPost]
        public IActionResult Edit(Vehicle vehicle)
        {
            const string pattern = @"[a-zA-Z\d-_]+$";
            Regex rg = new Regex(pattern);
            bool proceed = false;
            if(vehicle.Manufacturer != null)
            {
                if(rg.Match(vehicle.Manufacturer).Success)
                {
                    proceed = true;
                }
                else
                {
                    ModelState.AddModelError("Manufacturer", "Only Numbers, letters, Underscores, and Spaces in Manufacturer");
                }
            }

            if(vehicle.VIN != null)
            {
                foreach(char c in vehicle.VIN)
                {
                    if(!char.IsUpper(c) && !char.IsDigit(c))
                    {
                        ModelState.AddModelError("VIN", "VIN can only contain Numbers and Uppercases.");
                        break;
                    }
                }
            }

            if(ModelState.IsValid && proceed)
            { //checking model state

                VehicleHandler.UpdateVehicle(vehicle);
                return RedirectToAction("Summary");

            }
            return View(vehicle);
        }

        /// <summary>
        /// The view for the delete page
        /// </summary>
        /// <param name="vehicleId">The specified vehicles id</param>
        /// <returns>The view for the delete page with the specified vehicle object</returns>
        public IActionResult Delete(int vehicleId)
        {
            if(User.Identity.Name != null)
            {
                if(vehicleId != null)
                {
                    return View(VehicleHandler.GetVehicle(vehicleId));
                }
                return RedirectToAction("Summary", "Vehicles");
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The postback for the delete page
        /// </summary>
        /// <param name="id">The id of the vehicle being deleted</param>
        /// <param name="vehicle">Blank to differentiate from the delete view</param>
        /// <returns>To the summary page.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, [FromForm] Vehicle vehicle)
        {
            VehicleHandler.DeleteVehicle(id);
            return RedirectToAction("Summary");
        }
    }
}
