using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.Driver.Interfaces;
using VehicleAPI.Repositories.Driver.Models;
using VehicleAPI.Repositories.Driver.Requests;
using VehicleAPI.Repositories.Driver.Responses;
using VehicleAPI.Repositories.InsurancePolicy.Models;
using VehicleAPI.Repositories.InsurancePolicy.Requests;
using VehicleAPI.Repositories.InsurancePolicy.Responses;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _service;

        public DriverController(IDriverService service)
        {
            _service = service;
        }

        [HttpGet("GetAllDrivers")]
        public async Task<ActionResult<GetAllDriversResponse>> GetAllDrivers()
        {
            return await _service.GetAllDrivers();
        }

        [HttpPost("GetDriverById")]
        public async Task<ActionResult<DriverModel?>> GetDriverById(IdRequest request)
        {
            return await _service.GetDriverById(request);
        }

        [HttpPost("CreateDriver")]
        public async Task<ActionResult<OperationStatusResponse>> CreateDriver(CreateDriverRequest request)
        {
            return await _service.CreateDriver(request);
        }

        [HttpPost("UpdateDriver")]
        public async Task<ActionResult<OperationStatusResponse>> UpdateDriver(UpdateDriverRequest request)
        {
            return await _service.UpdateDriver(request);
        }

        [HttpPost("DeleteDriver")]
        public async Task<ActionResult<OperationStatusResponse>> DeleteDriver(IdRequest request)
        {
            return await _service.DeleteDriver(request);
        }
    }
}
