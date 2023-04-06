using Exsm3944_MySqlAuthentication.Data.Models;
using System.Linq;

namespace Exsm3944_MySqlAuthentication.Data
{
    public class VehicleHandler
    {

        /// <summary>
        /// Creates a vehicle with the provided specifications
        /// </summary>
        /// <param name="customerEmail">The customers email</param>
        /// <param name="VIN">The VIN of the vehicle</param>
        /// <param name="year">The model year</param>
        /// <param name="manufacturer"> The name of the manufacturer</param>
        /// <param name="colour"> The colour of the vehicle</param>
        /// <param name="model"> The name of the Model of the vehicle</param>
        /// <param name="purchaseDate"> The purchase date of the vehicle</param>
        /// <param name="SaleDate"> The sale date of the vehicle, default null</param>
        /// <returns>The newly created vehicle object.</returns>
        public static Vehicle CreateVehicle(string customerEmail, string VIN, int year, string manufacturer, string colour, string model, DateTime purchaseDate, DateTime? SaleDate = null )
        {
            Vehicle vehicle = new Vehicle();
            vehicle.CustomerEmail = customerEmail;
            vehicle.VIN = VIN;
            vehicle.ModelYear = year;
            vehicle.Manufacturer = manufacturer;
            vehicle.Model = model;
            vehicle.Colour = colour;
            vehicle.PurchaseDate = purchaseDate;
            vehicle.SaleDate = SaleDate;
            

            using(VehicleContext db = new VehicleContext())
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
            }

            return vehicle;
        }

        /// <summary>
        /// Gets all the Vehicles
        /// </summary>
        /// <returns>A list of all Vehicles</returns>
        public static List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using(VehicleContext db = new VehicleContext())
            {
                vehicles = db.Vehicles.ToList();
            }

            return vehicles;
        }

        /// <summary>
        /// Gets a vehicle at the requested id
        /// </summary>
        /// <param name="id">The id of the vehicles</param>
        /// <returns>The requested vehicle if exists, else a blank one</returns>
        public static Vehicle GetVehicle(int id)
        {
            Vehicle vehicle = new Vehicle();

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Vehicles.Find(id) != null)
                {
                    vehicle = db.Vehicles.Find(id);
                }
            }

            return vehicle;
        }

        /// <summary>
        /// Gets a list of vehicles for the specified customer
        /// </summary>
        /// <param name="email">The customer email attached to the vehicles</param>
        /// <returns>The requested vehicles if any exists, else a blank list</returns>
        public static List<Vehicle> GetVehicleByEmail(string email)
        {
            List<Vehicle> vehicle = new List<Vehicle>();

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Vehicles.Where(v => v.CustomerEmail == email) != null)
                {
                    vehicle = db.Vehicles.Where(v => v.CustomerEmail == email).ToList();
                }
            }

            return vehicle;
        }

        /// <summary>
        /// Updates the specified vehicle
        /// </summary>
        /// <param name="vehicle">The specified vehicle</param>
        /// <returns>True if succesful, else false</returns>
        public static bool UpdateVehicle(Vehicle vehicle)
        {
            Vehicle oldVehicle = new Vehicle();
            bool isUpdated = false;

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Vehicles.Find(vehicle.ID) != null)
                {
                    oldVehicle = db.Vehicles.Find(vehicle.ID);
                    oldVehicle.CustomerEmail = vehicle.CustomerEmail;
                    oldVehicle.VIN = vehicle.VIN;
                    oldVehicle.ModelYear = vehicle.ModelYear;
                    oldVehicle.Manufacturer = vehicle.Manufacturer;
                    oldVehicle.Model = vehicle.Model;
                    oldVehicle.Colour = vehicle.Colour;
                    oldVehicle.PurchaseDate = vehicle.PurchaseDate;
                    oldVehicle.SaleDate = vehicle.SaleDate;

                    db.SaveChanges();
                    isUpdated = true;
                }
            }

            return isUpdated;
        }

        /// <summary>
        /// Deletes the specified vehicle
        /// </summary>
        /// <param name="vehicle">The specified vehicle</param>
        /// <returns>True if succesful, else false</returns>
        public static bool DeleteVehicle(int id)
        {
            bool isDeleted = false;

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Vehicles.Find(id) != null)
                {
                    Vehicle deleted = db.Vehicles.Single(x => x.ID == id);
                    db.Vehicles.Remove(deleted);
                    db.SaveChanges();
                    isDeleted = true;
                }
            }

            return isDeleted;
        }
    }
}
