using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.Repositories.MotorVehicle.Requests
{
    public class UpdateMotorVehicleRequest
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int MotorVehicleTypeId { get; set; }
        public int InsurancePolicyId { get; set; }
        public List<int> SelectedDriversIds { get; set; } = [];
    }
}
