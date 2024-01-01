namespace VehicleAPI.Repositories.InsurancePolicy.Requests
{
    public class UpdateInsurancePolicyRequest
    {
        public int Id { get; set; }
        public string PolicyProvider { get; set; } = string.Empty;
    }
}
