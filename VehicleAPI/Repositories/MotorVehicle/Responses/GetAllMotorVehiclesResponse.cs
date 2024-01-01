using VehicleAPI.Repositories.MotorVehicle.Models;

namespace VehicleAPI.Repositories.MotorVehicle.Responses
{
    public class GetAllMotorVehiclesResponse
    {
        public List<MotorVehicleDto> List { get; set; } = new();
    }
}
