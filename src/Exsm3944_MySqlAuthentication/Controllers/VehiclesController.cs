using Exsm3944_MySqlAuthentication.Data;
using Exsm3944_MySqlAuthentication.Models;
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
                List<VehicleModel> models = VehicleModelHandler.GetVehicleModelByEmail(User.Identity.Name);
                if(models.Count() == 0)
                {
                    return RedirectToAction("Create", "VehicleModel");
                }
                ViewBag.VehicleModels = models;
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
            if(vehicle.VIN != vehicle.VIN.ToUpper())
            {
                ModelState.AddModelError("VIN", "VIN must be all caps");
            }   
            
            if(vehicle.ModelYear >= DateTime.Now.Year + 1)
            {
                ModelState.AddModelError("ModelYear", "Model Year must be less than or equal to the current year plus one.");
            }

            if(vehicle.PurchaseDate >= DateTime.Now)
            {
                ModelState.AddModelError("PurchaseDate", "Purchase Date must in the past or the current time.");
            }

            if(vehicle.SaleDate != null)
            {
                if(vehicle.SaleDate > vehicle.PurchaseDate)
                {
                    ModelState.AddModelError("SaleDate", "Sale Date must be after the purchase date.");
                }
            }
            ModelState.Remove("VehicleModel");

            if(ModelState.IsValid)
            { //checking model state
                vehicle.CustomerEmail = User.Identity.Name;
                VehicleHandler.CreateVehicle(vehicle.CustomerEmail, vehicle.VIN, vehicle.ModelYear,  vehicle.Colour, vehicle.ModelID, vehicle.PurchaseDate, vehicle.SaleDate);
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
                    ViewBag.VehicleModels = VehicleModelHandler.GetVehicleModelByEmail(User.Identity.Name);
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

            if(vehicle.VIN != vehicle.VIN.ToUpper())
            {
                ModelState.AddModelError("VIN", "VIN must be all caps");
            }

            if(vehicle.ModelYear >= DateTime.Now.Year + 1)
            {
                ModelState.AddModelError("ModelYear", "Model Year must be less than or equal to the current year plus one.");
            }

            if(vehicle.PurchaseDate >= DateTime.Now)
            {
                ModelState.AddModelError("PurchaseDate", "Purchase Date must in the past or the current time.");
            }

            if(vehicle.SaleDate != null)
            {
                if(vehicle.SaleDate > vehicle.PurchaseDate)
                {
                    ModelState.AddModelError("SaleDate", "Sale Date must be after the purchase date.");
                }
            }
            ModelState.Remove("VehicleModel");

            if(ModelState.IsValid)
            { //checking model state

                vehicle.CustomerEmail = User.Identity.Name;
                VehicleHandler.UpdateVehicle(vehicle);
                return RedirectToAction("Summary", "Vehicles");

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
