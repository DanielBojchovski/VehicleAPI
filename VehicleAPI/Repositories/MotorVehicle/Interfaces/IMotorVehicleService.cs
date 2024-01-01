using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.MotorVehicle.Models;
using VehicleAPI.Repositories.MotorVehicle.Requests;
using VehicleAPI.Repositories.MotorVehicle.Responses;

namespace VehicleAPI.Repositories.MotorVehicle.Interfaces
{
    public interface IMotorVehicleService
    {
        Task<GetAllMotorVehiclesResponse> GetAllMotorVehicles();
        Task<MotorVehicleModel?> GetMotorVehicleById(IdRequest request);
        Task<OperationStatusResponse> CreateMotorVehicle(CreateMotorVehicleRequest request);
        Task<OperationStatusResponse> UpdateMotorVehicle(UpdateMotorVehicleRequest request);
        Task<OperationStatusResponse> DeleteMotorVehicle(IdRequest request);
    }
}
