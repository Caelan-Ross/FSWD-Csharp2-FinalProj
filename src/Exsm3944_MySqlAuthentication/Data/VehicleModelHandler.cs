using Exsm3944_MySqlAuthentication.Models;

namespace Exsm3944_MySqlAuthentication.Data
{
    public class VehicleModelHandler
    {
        /// <summary>
        /// Creates a vehicle model with the provided specifications
        /// </summary>
        /// <param name="name">The name of the vehicle model</param>
        /// <param name="manufacturerId">The id of the parent manufacturer for the vehicle model</param>
        /// <returns>The newly created vehicle model</returns>
        public static VehicleModel CreateVehicleModel(string name, int manufacturerId, string customerEmail)
        {
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.Name = name;
            vehicleModel.ManufacturerID = manufacturerId;
            vehicleModel.CustomerEmail = customerEmail;

            using(VehicleContext db = new VehicleContext())
            {
                db.VehicleModels.Add(vehicleModel);
                db.SaveChanges();
            }

            return vehicleModel;
        }

        /// <summary>
        /// Gets all the VehicleModels
        /// </summary>
        /// <returns>A list of all VehicleModels</returns>
        public static List<VehicleModel> GetAllVehicleModels()
        {
            List<VehicleModel> vehicleModels = new List<VehicleModel>();

            using(VehicleContext db = new VehicleContext())
            {
                vehicleModels = db.VehicleModels.ToList();
            }

            return vehicleModels;
        }

        /// <summary>
        /// Gets the specified vehicle model
        /// </summary>
        /// <param name="id">The id of the model being searched for</param>
        /// <returns>The requested vehicle model if success, else a blank one</returns>
        public static VehicleModel GetVehicleModel(int id)
        {
            VehicleModel vehicleModel = new VehicleModel();

            using(VehicleContext db = new VehicleContext())
            {
                if(db.VehicleModels.Find(id) != null)
                {
                    vehicleModel = db.VehicleModels.Find(id);
                }
            }

            return vehicleModel;
        }

        /// <summary>
        /// Updates the specified vehicle model
        /// </summary>
        /// <param name="vehicleModel">The specified vehicle model</param>
        /// <returns>True if successful, else false</returns>
        public static bool UpdateVehicleModel(VehicleModel vehicleModel)
        {
            VehicleModel oldVehicleModel = new VehicleModel();
            bool isUpdated = false;

            using(VehicleContext db = new VehicleContext())
            {
                if(db.VehicleModels.Find(vehicleModel.ID) != null)
                {
                    oldVehicleModel = db.VehicleModels.Find(vehicleModel.ID);
                    oldVehicleModel.Name = vehicleModel.Name;
                    oldVehicleModel.ManufacturerID = vehicleModel.ManufacturerID;
                    oldVehicleModel.CustomerEmail = vehicleModel.CustomerEmail;
                    db.SaveChanges();
                    isUpdated = true;
                }
            }

            return isUpdated;
        }

        /// <summary>
        /// Gets a list of Vehicle Models for the specified customer
        /// </summary>
        /// <param name="email">The customer email attached to the Vehicle Models</param>
        /// <returns>The requested Vehicle Models if any exists, else a blank list</returns>
        public static List<VehicleModel> GetVehicleByEmail(string email)
        {
            List<VehicleModel> vehicleModels = new List<VehicleModel>();

            using(VehicleContext db = new VehicleContext())
            {
                if(db.VehicleModels.Where(v => v.CustomerEmail == email) != null)
                {
                    vehicleModels = db.VehicleModels.Where(v => v.CustomerEmail == email).ToList();
                }
            }

            return vehicleModels;
        }

        /// <summary>
        /// Deletes the specified vehicle mdoel.
        /// </summary>
        /// <param name="vehicleModel">The specified vehicle model</param>
        /// <returns>True if succesful, else false</returns>
        public static bool DeleteVehicleModel(VehicleModel vehicleModel)
        {
            bool isDeleted = false;
            bool canBeDeleted = true;

            List<Vehicle> vehicles = VehicleHandler.GetAllVehicles();

            foreach(Vehicle vehicle in vehicles)
            {
                if(vehicle.ModelID == vehicleModel.ID)
                {
                    canBeDeleted = false;
                }
            }

            if(canBeDeleted)
            {
                using(VehicleContext db = new VehicleContext())
                {
                    if(db.VehicleModels.Find(vehicleModel.ID) != null)
                    {
                        VehicleModel deleted = db.VehicleModels.Single(x => x.ID == vehicleModel.ID);
                        db.VehicleModels.Remove(deleted);
                        db.SaveChanges();
                        isDeleted = true;
                    }
                }
            }

            return isDeleted;
        }
    }
}
