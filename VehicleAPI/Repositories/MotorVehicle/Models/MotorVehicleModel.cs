using VehicleAPI.Repositories.Driver.Models;

namespace VehicleAPI.Repositories.MotorVehicle.Models
{
    public class MotorVehicleModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int MotorVehicleTypeId { get; set; }
        public int InsurancePolicyId { get; set; }
        public List<DriverModel> Drivers { get; set; } = new();
    }
}
