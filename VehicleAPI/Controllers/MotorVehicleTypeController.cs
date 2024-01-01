using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.MotorVehicleType.Interfaces;
using VehicleAPI.Repositories.MotorVehicleType.Models;
using VehicleAPI.Repositories.MotorVehicleType.Requests;
using VehicleAPI.Repositories.MotorVehicleType.Responses;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorVehicleTypeController : ControllerBase
    {
        private readonly IMotorVehicleTypeService _service;

        public MotorVehicleTypeController(IMotorVehicleTypeService service)
        {
            _service = service;
        }

        [HttpGet("GetAllMotorVehicleTypes")]
        public async Task<ActionResult<GetAllMotorVehicleTypesResponse>> GetAllMotorVehicleTypes()
        {
            return await _service.GetAllMotorVehicleTypes();
        }

        [HttpPost("GetMotorVehicleTypeById")]
        public async Task<ActionResult<MotorVehicleTypeModel?>> GetMotorVehicleTypeById(IdRequest request)
        {
            return await _service.GetMotorVehicleTypeById(request);
        }

        [HttpPost("CreateMotorVehicleType")]
        public async Task<ActionResult<OperationStatusResponse>> CreateMotorVehicleType(CreateMotorVehicleTypeRequest request)
        {
            return await _service.CreateMotorVehicleType(request);
        }

        [HttpPost("UpdateMotorVehicleType")]
        public async Task<ActionResult<OperationStatusResponse>> UpdateMotorVehicleType(UpdateMotorVehicleTypeRequest request)
        {
            return await _service.UpdateMotorVehicleType(request);
        }

        [HttpPost("DeleteMotorVehicleType")]
        public async Task<ActionResult<OperationStatusResponse>> DeleteMotorVehicleType(IdRequest request)
        {
            return await _service.DeleteMotorVehicleType(request);
        }
    }
}
