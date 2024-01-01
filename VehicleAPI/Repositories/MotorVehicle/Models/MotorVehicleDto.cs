using VehicleAPI.Repositories.Driver.Models;

namespace VehicleAPI.Repositories.MotorVehicle.Models
{
    public class MotorVehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string MotorVehicleType { get; set; } = string.Empty;
        public string InsurancePolicy { get; set; } = string.Empty;
        public List<DriverModel> Drivers { get; set; } = new();
    }
}
