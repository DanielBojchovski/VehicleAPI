using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Repositories.MotorVehicleType.Models;
using VehicleAPI.Repositories.MotorVehicleType.Requests;
using VehicleAPI.Repositories.MotorVehicleType.Responses;

namespace VehicleAPI.Repositories.MotorVehicleType.Interfaces
{
    public interface IMotorVehicleTypeService
    {
        Task<GetAllMotorVehicleTypesResponse> GetAllMotorVehicleTypes();
        Task<MotorVehicleTypeModel?> GetMotorVehicleTypeById(IdRequest request);
        Task<OperationStatusResponse> CreateMotorVehicleType(CreateMotorVehicleTypeRequest request);
        Task<OperationStatusResponse> UpdateMotorVehicleType(UpdateMotorVehicleTypeRequest request);
        Task<OperationStatusResponse> DeleteMotorVehicleType(IdRequest request);
    }
}
