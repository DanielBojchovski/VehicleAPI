using VehicleAPI.Repositories.InsurancePolicy.Models;

namespace VehicleAPI.Repositories.InsurancePolicy.Responses
{
    public class GetInsurancePoliciesForMotorVehicleResponse
    {
        public List<InsurancePolicyModel> List { get; set; } = new();
    }
}
