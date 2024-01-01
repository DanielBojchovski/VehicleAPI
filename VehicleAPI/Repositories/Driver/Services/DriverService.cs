using Azure;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Entities;
using VehicleAPI.Repositories.Driver.Interfaces;
using VehicleAPI.Repositories.Driver.Models;
using VehicleAPI.Repositories.Driver.Requests;
using VehicleAPI.Repositories.Driver.Responses;

namespace VehicleAPI.Repositories.Driver.Services
{
    public class DriverService : IDriverService
    {
        private readonly VehicleDBContext _context;

        public DriverService(VehicleDBContext context)
        {
            _context = context;
        }

        public async Task<GetAllDriversResponse> GetAllDrivers()
        {
            var list = await _context.Driver
                   .AsNoTracking()
                   .Select(x => new DriverModel
                   {
                       Id = x.Id,
                       FirstName = x.FirstName,
                       LastName = x.LastName
                   }).ToListAsync();

            return new GetAllDriversResponse { List = list };
        }

        public async Task<DriverModel?> GetDriverById(IdRequest request)
        {
            var response = await _context.Driver
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .Select(x => new DriverModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).FirstOrDefaultAsync();

            return response;
        }

        public async Task<OperationStatusResponse> CreateDriver(CreateDriverRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid driver first name." };

            if (string.IsNullOrWhiteSpace(request.LastName))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid driver last name." };

            try
            {
                VehicleAPI.Entities.Driver driverDto = new() { FirstName = request.FirstName, LastName = request.LastName };

                _context.Add(driverDto);
                await _context.SaveChangesAsync();

                return new OperationStatusResponse { IsSuccessful = true, Message = "Success. Driver created successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> UpdateDriver(UpdateDriverRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid driver first name." };

            if (string.IsNullOrWhiteSpace(request.LastName))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid driver last name." };

            try
            {
                var driverDto = await _context.Driver.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (driverDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Driver with ID {request.Id} not found." };

                driverDto.FirstName = request.FirstName;
                driverDto.LastName = request.LastName;

                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Driver with ID {driverDto.Id} updated successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> DeleteDriver(IdRequest request)
        {
            try
            {
                var driverDto = await _context.Driver.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (driverDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Driver with ID {request.Id} not found." };

                _context.Remove(driverDto);
                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Driver with ID {driverDto.Id} deleted successfully." };
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
