using Microsoft.EntityFrameworkCore;
using System;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Entities;
using VehicleAPI.Repositories.MotorVehicleType.Interfaces;
using VehicleAPI.Repositories.MotorVehicleType.Models;
using VehicleAPI.Repositories.MotorVehicleType.Requests;
using VehicleAPI.Repositories.MotorVehicleType.Responses;

namespace VehicleAPI.Repositories.MotorVehicleType.Services
{
    public class MotorVehicleTypeService : IMotorVehicleTypeService
    {
        private readonly VehicleDBContext _context;

        public MotorVehicleTypeService(VehicleDBContext context)
        {
            _context = context;
        }

        public async Task<GetAllMotorVehicleTypesResponse> GetAllMotorVehicleTypes()
        {
            var list = await _context.MotorVehicleType
                    .AsNoTracking()
                    .Select(x => new MotorVehicleTypeModel
                    {
                        Id = x.Id,
                        Type = x.Type
                    }).ToListAsync();

            return new GetAllMotorVehicleTypesResponse { List = list };
        }

        public async Task<MotorVehicleTypeModel?> GetMotorVehicleTypeById(IdRequest request)
        {
            var response = await _context.MotorVehicleType
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .Select(x => new MotorVehicleTypeModel
                {
                    Id = x.Id,
                    Type = x.Type
                }).FirstOrDefaultAsync();

            return response;
        }

        public async Task<OperationStatusResponse> CreateMotorVehicleType(CreateMotorVehicleTypeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Type))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid type name." };

            try
            {
                VehicleAPI.Entities.MotorVehicleType motorVehicleTypeDto = new() { Type = request.Type };

                _context.Add(motorVehicleTypeDto);
                await _context.SaveChangesAsync();

                return new OperationStatusResponse { IsSuccessful = true, Message = "Success. MotorVehicleType created successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> UpdateMotorVehicleType(UpdateMotorVehicleTypeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Type))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid type name." };

            try
            {
                var motorVehicleTypeDto = await _context.MotorVehicleType.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (motorVehicleTypeDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Motor vehicle type with ID {request.Id} not found." };

                motorVehicleTypeDto.Type = request.Type;
                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Motor vehicle type with ID {motorVehicleTypeDto.Id} updated successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> DeleteMotorVehicleType(IdRequest request)
        {
            try
            {
                var motorVehicleTypeDto = await _context.MotorVehicleType.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (motorVehicleTypeDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Motor vehicle type with ID {request.Id} not found." };

                _context.Remove(motorVehicleTypeDto);
                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Motor vehicle type with ID {motorVehicleTypeDto.Id} deleted successfully." };
            }
            catch (DbUpdateException)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = "You can't delete an entity which is referenced in another table" };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}
