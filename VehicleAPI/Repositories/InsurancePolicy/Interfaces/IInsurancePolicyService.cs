using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.InsurancePolicy.Models;
using VehicleAPI.Repositories.InsurancePolicy.Requests;
using VehicleAPI.Repositories.InsurancePolicy.Responses;

namespace VehicleAPI.Repositories.InsurancePolicy.Interfaces
{
    public interface IInsurancePolicyService
    {
        Task<GetAllInsurancePoliciesResponse> GetAllInsurancePolicies();
        Task<InsurancePolicyModel?> GetInsurancePolicyById(IdRequest request);
        Task<OperationStatusResponse> CreateInsurancePolicy(CreateInsurancePolicyRequest request);
        Task<OperationStatusResponse> UpdateInsurancePolicy(UpdateInsurancePolicyRequest request);
        Task<OperationStatusResponse> DeleteInsurancePolicy(IdRequest request);
        Task<GetInsurancePoliciesForMotorVehicleResponse> GetInsurancePoliciesForMotorVehicleCreate();
        Task<GetInsurancePoliciesForMotorVehicleResponse> GetInsurancePoliciesForMotorVehicleUpdate(IdRequest request);
    }
}
