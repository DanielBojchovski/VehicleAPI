using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.MotorVehicle.Interfaces;
using VehicleAPI.Repositories.MotorVehicle.Models;
using VehicleAPI.Repositories.MotorVehicle.Requests;
using VehicleAPI.Repositories.MotorVehicle.Responses;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorVehicleController : ControllerBase
    {
        private readonly IMotorVehicleService _service;

        public MotorVehicleController(IMotorVehicleService service)
        {
            _service = service;
        }

        [HttpGet("GetAllMotorVehicles")]
        public async Task<ActionResult<GetAllMotorVehiclesResponse>> GetAllMotorVehicles()
        {
            return await _service.GetAllMotorVehicles();
        }

        [HttpPost("GetMotorVehicleById")]
        public async Task<ActionResult<MotorVehicleModel?>> GetMotorVehicleById(IdRequest request)
        {
            return await _service.GetMotorVehicleById(request);
        }

        [HttpPost("CreateMotorVehicle")]
        public async Task<ActionResult<OperationStatusResponse>> CreateMotorVehicle(CreateMotorVehicleRequest request)
        {
            return await _service.CreateMotorVehicle(request);
        }

        [HttpPost("UpdateMotorVehicle")]
        public async Task<ActionResult<OperationStatusResponse>> UpdateMotorVehicle(UpdateMotorVehicleRequest request)
        {
            return await _service.UpdateMotorVehicle(request);
        }

        [HttpPost("DeleteMotorVehicle")]
        public async Task<ActionResult<OperationStatusResponse>> DeleteMotorVehicle(IdRequest request)
        {
            return await _service.DeleteMotorVehicle(request);
        }
    }
}
