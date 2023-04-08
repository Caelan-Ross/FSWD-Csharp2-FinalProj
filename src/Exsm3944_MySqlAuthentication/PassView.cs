using Exsm3944_MySqlAuthentication.Models;

namespace Exsm3944_MySqlAuthentication
{
    public class PassView
    {
        public PassView()
        {
            Manufacturer = new Manufacturer();
            Vehicle = new Vehicle();
            VehicleModel = new VehicleModel();
        }

        public Manufacturer Manufacturer { get; set; }

        public Vehicle Vehicle { get; set; }

        public VehicleModel VehicleModel { get; set; }
    }
}
