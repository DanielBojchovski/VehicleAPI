using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.InsurancePolicy.Interfaces;
using VehicleAPI.Repositories.InsurancePolicy.Models;
using VehicleAPI.Repositories.InsurancePolicy.Requests;
using VehicleAPI.Repositories.InsurancePolicy.Responses;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly IInsurancePolicyService _service;

        public InsurancePolicyController(IInsurancePolicyService service)
        {
            _service = service;
        }

        [HttpGet("GetAllInsurancePolicies")]
        public async Task<ActionResult<GetAllInsurancePoliciesResponse>> GetAllInsurancePolicies()
        {
            return await _service.GetAllInsurancePolicies();
        }

        [HttpPost("GetInsurancePolicyById")]
        public async Task<ActionResult<InsurancePolicyModel?>> GetInsurancePolicyById(IdRequest request)
        {
            return await _service.GetInsurancePolicyById(request);
        }

        [HttpPost("CreateInsurancePolicy")]
        public async Task<ActionResult<OperationStatusResponse>> CreateInsurancePolicy(CreateInsurancePolicyRequest request)
        {
            return await _service.CreateInsurancePolicy(request);
        }

        [HttpPost("UpdateInsurancePolicy")]
        public async Task<ActionResult<OperationStatusResponse>> UpdateInsurancePolicy(UpdateInsurancePolicyRequest request)
        {
            return await _service.UpdateInsurancePolicy(request);
        }

        [HttpPost("DeleteInsurancePolicy")]
        public async Task<ActionResult<OperationStatusResponse>> DeleteInsurancePolicy(IdRequest request)
        {
            return await _service.DeleteInsurancePolicy(request);
        }

        [HttpGet("GetInsurancePoliciesForMotorVehicleCreate")]
        public async Task<ActionResult<GetInsurancePoliciesForMotorVehicleResponse>> GetInsurancePoliciesForMotorVehicleCreate()
        {
            return await _service.GetInsurancePoliciesForMotorVehicleCreate();
        }

        [HttpPost("GetInsurancePoliciesForMotorVehicleUpdate")]
        public async Task<ActionResult<GetInsurancePoliciesForMotorVehicleResponse>> GetInsurancePoliciesForMotorVehicleUpdate(IdRequest request)
        {
            return await _service.GetInsurancePoliciesForMotorVehicleUpdate(request);
        }
    }
}
