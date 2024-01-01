namespace VehicleAPI.Repositories.MotorVehicle.Requests
{
    public class CreateMotorVehicleRequest
    {
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int MotorVehicleTypeId { get; set; }
        public int InsurancePolicyId { get; set; }
        public List<int> SelectedDriversIds { get; set; } = [];
    }
}
