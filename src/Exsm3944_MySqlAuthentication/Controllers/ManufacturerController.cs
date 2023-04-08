using Exsm3944_MySqlAuthentication.Data;
using Exsm3944_MySqlAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Exsm3944_MySqlAuthentication.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: ManufacturerController
        public ActionResult Index()
        {
            if(User.Identity.Name != null)
            {
                ViewBag.Email = User.Identity.Name;
                return View(ManufacturerHandler.GetManufacturerByEmail(User.Identity.Name));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ManufacturerController/Details/5
        public ActionResult Details(int id)
        {
            if(User.Identity.Name != null)
            {
                if(id != null)
                {
                    return View(ManufacturerHandler.GetManufacturer(id));
                }
                return RedirectToAction("Index", "Manufacturer");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ManufacturerController/Create
        public ActionResult Create()
        {
            if(User.Identity.Name != null)
            {
                Manufacturer manufacturer = new Manufacturer();
                manufacturer.ID = 0;
                manufacturer.CustomerEmail = User.Identity.Name;
                return View(manufacturer);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: ManufacturerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturer manufacturer)
        {
            manufacturer.CustomerEmail = User.Identity.Name;
            ModelState.Remove("VehicleModels");
            ModelState.Remove("CustomerEmail");
            if(ModelState.IsValid)
            {
                ManufacturerHandler.CreateManufacturer(manufacturer.Name, manufacturer.CustomerEmail);
                return RedirectToAction("Index", "Manufacturer");
            }
            return View(manufacturer);
        }

        // GET: ManufacturerController/Edit/5
        public ActionResult Edit(int id)
        {
            if(User.Identity.Name != null)
            {
                if(id != null)
                {
                    return View(ManufacturerHandler.GetManufacturer(id));
                }
                return RedirectToAction("Index", "Manufacturer");
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: ManufacturerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            ModelState.Remove("VehicleModels");
            ModelState.Remove("CustomerEmail");
            if(ModelState.IsValid)
            {
                manufacturer.CustomerEmail = User.Identity.Name;
                ManufacturerHandler.UpdateManufacturer(manufacturer);
                return RedirectToAction("Index", "Manufacturer");
            }
            return View(manufacturer);
        }

        // GET: ManufacturerController/Delete/5
        public ActionResult Delete(int id)
        {
            if(User.Identity.Name != null)
            {
                if(id != null)
                {
                    return View(ManufacturerHandler.GetManufacturer(id));
                }
                return RedirectToAction("Index", "Manufacturer");
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: ManufacturerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, [FromForm] Manufacturer manufacturer)
        {
            ManufacturerHandler.DeleteManufacturer(manufacturer);
            return RedirectToAction("Index", "Manufacturer");
        }
    }
}
