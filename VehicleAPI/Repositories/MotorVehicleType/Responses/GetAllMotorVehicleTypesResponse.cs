using VehicleAPI.Repositories.MotorVehicleType.Models;

namespace VehicleAPI.Repositories.MotorVehicleType.Responses
{
    public class GetAllMotorVehicleTypesResponse
    {
        public List<MotorVehicleTypeModel> List { get; set; } = new();
    }
}
