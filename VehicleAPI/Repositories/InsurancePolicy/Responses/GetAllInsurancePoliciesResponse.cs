using VehicleAPI.Repositories.InsurancePolicy.Models;

namespace VehicleAPI.Repositories.InsurancePolicy.Responses
{
    public class GetAllInsurancePoliciesResponse
    {
        public List<InsurancePolicyModel> List { get; set; } = new();
    }
}
