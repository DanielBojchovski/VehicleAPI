using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.Driver.Models;
using VehicleAPI.Repositories.Driver.Requests;
using VehicleAPI.Repositories.Driver.Responses;

namespace VehicleAPI.Repositories.Driver.Interfaces
{
    public interface IDriverService
    {
        Task<GetAllDriversResponse> GetAllDrivers();
        Task<DriverModel?> GetDriverById(IdRequest request);
        Task<OperationStatusResponse> CreateDriver(CreateDriverRequest request);
        Task<OperationStatusResponse> UpdateDriver(UpdateDriverRequest request);
        Task<OperationStatusResponse> DeleteDriver(IdRequest request);
    }
}
