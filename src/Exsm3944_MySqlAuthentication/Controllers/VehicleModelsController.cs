using Exsm3944_MySqlAuthentication.Data;
using Exsm3944_MySqlAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exsm3944_MySqlAuthentication.Controllers
{
    public class VehicleModelsController : Controller
    {
        // GET: VehicleModelsController
        public ActionResult Index()
        {
            if(User.Identity.Name != null)
            {
                ViewBag.Email = User.Identity.Name;
                return View(VehicleModelHandler.GetVehicleModelByEmail(User.Identity.Name));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: VehicleModelsController/Details/5
        public ActionResult Details(int id)
        {
            if(User.Identity.Name != null)
            {
                if(id != null)
                {
                    return View(VehicleModelHandler.GetVehicleModel(id));
                }

                return RedirectToAction("Index", "VehicleModels");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: VehicleModelsController/Create
        public ActionResult Create()
        {
            if(User.Identity.Name != null)
            {
                List<Manufacturer> manufacturers = ManufacturerHandler.GetManufacturerByEmail(User.Identity.Name);
                if(manufacturers.Count() == 0)
                {
                    return RedirectToAction("Create", "Manufacturer");
                }
                ViewBag.Manufacturers = manufacturers;
                VehicleModel vehicleModel = new VehicleModel();
                vehicleModel.ID = 0;
                vehicleModel.CustomerEmail = User.Identity.Name;
                return View(vehicleModel);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: VehicleModelsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleModel vehicleModel)
        {
            ModelState.Remove("Vehicles");
            ModelState.Remove("Manufacturer");
            if(ModelState.IsValid)
            {
                VehicleModelHandler.CreateVehicleModel(vehicleModel.Name, vehicleModel.ManufacturerID, vehicleModel.CustomerEmail);
                return RedirectToAction("Index");
            }

            return View(vehicleModel);
        }

        // GET: VehicleModelsController/Edit/5
        public ActionResult Edit(int id)
        {
            if(User.Identity.Name != null)
            {
                if(id != null)
                {
                    ViewBag.Manufacturers = ManufacturerHandler.GetManufacturerByEmail(User.Identity.Name);
                    return View(VehicleModelHandler.GetVehicleModel(id));
                }
                return RedirectToAction("Index", "VehicleModels");
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: VehicleModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VehicleModel vehicleModel)
        {
            ModelState.Remove("Vehicles");
            ModelState.Remove("Manufacturer");
            if(ModelState.IsValid)
            {
                vehicleModel.CustomerEmail = User.Identity.Name;
                VehicleModelHandler.UpdateVehicleModel(vehicleModel);
                return RedirectToAction("Index", "VehicleModels");
            }

            return View(vehicleModel);
        }

        // GET: VehicleModelsController/Delete/5
        public ActionResult Delete(int id)
        {
            if(User.Identity.Name != null)
            {
                if(id != null)
                {
                    return View(VehicleModelHandler.GetVehicleModel(id));
                }
                return RedirectToAction("Index", "VehicleModels");
            }
            return RedirectToAction("Index", "Home"); ;
        }

        // POST: VehicleModelsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] VehicleModel vehicleModel)
        {
            VehicleModelHandler.DeleteVehicleModel(vehicleModel);
            return RedirectToAction("Index");
        }
    }
}
