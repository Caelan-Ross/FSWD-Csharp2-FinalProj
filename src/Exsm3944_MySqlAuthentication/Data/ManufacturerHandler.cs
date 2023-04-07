using Exsm3944_MySqlAuthentication.Models;

namespace Exsm3944_MySqlAuthentication.Data
{
    public class ManufacturerHandler
    {
        /// <summary>
        /// Creates a Manufacturer with the specified params.
        /// </summary>
        /// <param name="name">The name of the manufacturer</param>
        /// <returns>The new manufacturer</returns>
        public static Manufacturer CreateManufacturer(string name, string customerEmail)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Name = name;
            manufacturer.CustomerEmail = customerEmail;

            using(VehicleContext db = new VehicleContext())
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
            }

            return manufacturer;
        }

        /// <summary>
        /// Gets all the manufacturers
        /// </summary>
        /// <returns>A list of all manufacturers</returns>
        public static List<Manufacturer> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            using(VehicleContext db = new VehicleContext())
            {
                manufacturers = db.Manufacturers.ToList();
            }

            return manufacturers;
        }

        /// <summary>
        /// Gets the manufacturer at the specifed id if exists.
        /// </summary>
        /// <param name="id">The id of the manufacturer</param>
        /// <returns>The specfied manufacturer if exists, else returns blank manufacturer.</returns>
        public static Manufacturer GetManufacturer(int id)
        {
            Manufacturer manufacturer = new Manufacturer();

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Manufacturers.Find(id) != null)
                {
                    manufacturer = db.Manufacturers.Find(id);
                }
            }

            return manufacturer;
        }

        /// <summary>
        /// Gets a list of manufacturers for the specified customer
        /// </summary>
        /// <param name="email">The customer email attached to the manufacturers</param>
        /// <returns>The requested manufacturers if any exists, else a blank list</returns>
        public static List<Manufacturer> GetManufacturerByEmail(string email)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Manufacturers.Where(v => v.CustomerEmail == email) != null)
                {
                    manufacturers = db.Manufacturers.Where(v => v.CustomerEmail == email).ToList();
                }
            }

            return manufacturers;
        }

        /// <summary>
        /// Updates the provided manufacturer.
        /// </summary>
        /// <param name="manufacturer">The manufacturer to be updated</param>
        /// <returns> True if succeed, otherwise false</returns>
        public static bool UpdateManufacturer(Manufacturer manufacturer)
        {
            Manufacturer oldManufacturer = new Manufacturer();
            bool isUdpated = false;

            using(VehicleContext db = new VehicleContext())
            {
                if(db.Manufacturers.Find(manufacturer.ID) != null)
                {
                    oldManufacturer = db.Manufacturers.Find(manufacturer.ID);
                    oldManufacturer.Name = manufacturer.Name;
                    oldManufacturer.CustomerEmail = manufacturer.CustomerEmail;
                    db.SaveChanges();
                    isUdpated = true;
                }
            }

            return isUdpated;
        }

        /// <summary>
        /// Delets the specified manufacturer.
        /// </summary>
        /// <param name="manufacturer">The provided manufacturer.</param>
        /// <returns>True if successfully deleted, else false</returns>
        public static bool DeleteManufacturer(Manufacturer manufacturer)
        {
            bool isDeleted = false;
            bool canBeDeleted = true;

            List<VehicleModel> models = VehicleModelHandler.GetAllVehicleModels();

            foreach(VehicleModel model in models)
            {
                if(model.ManufacturerID == manufacturer.ID)
                {
                    canBeDeleted = false;
                }
            }

            if(canBeDeleted)
            {
                using(VehicleContext db = new VehicleContext())
                {
                    if(db.Manufacturers.Find(manufacturer.ID) != null)
                    {
                        Manufacturer deleted = db.Manufacturers.Single(x => x.ID == manufacturer.ID);
                        db.Manufacturers.Remove(deleted);
                        db.SaveChanges();
                        isDeleted = true;
                    }
                }
            }
            return isDeleted;
        }
    }
}
