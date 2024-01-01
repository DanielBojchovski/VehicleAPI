using Azure;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Entities;
using VehicleAPI.Repositories.Driver.Models;
using VehicleAPI.Repositories.MotorVehicle.Interfaces;
using VehicleAPI.Repositories.MotorVehicle.Models;
using VehicleAPI.Repositories.MotorVehicle.Requests;
using VehicleAPI.Repositories.MotorVehicle.Responses;

namespace VehicleAPI.Repositories.MotorVehicle.Services
{
    public class MotorVehicleService : IMotorVehicleService
    {
        private readonly VehicleDBContext _context;

        public MotorVehicleService(VehicleDBContext context)
        {
            _context = context;
        }

        public async Task<GetAllMotorVehiclesResponse> GetAllMotorVehicles()
        {
            var list = await _context.MotorVehicle
                    .AsNoTracking()
                    .Select(x => new MotorVehicleDto
                    {
                        Id = x.Id,
                        Brand = x.Brand,
                        Model = x.Model,
                        MotorVehicleType = x.MotorVehicleType.Type,
                        InsurancePolicy = x.InsurancePolicy.PolicyProvider,
                        Drivers = x.MotorVehicleDriver.Where(y => y.MotorVehicleId == x.Id).Select(y => new DriverModel
                        {
                            Id = y.DriverId,
                            FirstName = y.Driver.FirstName,
                            LastName = y.Driver.LastName
                        }).ToList()
                    }).ToListAsync();

            return new GetAllMotorVehiclesResponse { List = list };
        }

        public async Task<MotorVehicleModel?> GetMotorVehicleById(IdRequest request)
        {
            var response = await _context.MotorVehicle
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .Select(x => new MotorVehicleModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    MotorVehicleTypeId = x.MotorVehicleTypeId,
                    InsurancePolicyId = x.InsurancePolicyId,
                    Drivers = x.MotorVehicleDriver.Where(y => y.MotorVehicleId == x.Id).Select(y => new DriverModel
                    {
                        Id = y.DriverId,
                        FirstName = y.Driver.FirstName,
                        LastName = y.Driver.LastName
                    }).ToList()
                }).FirstOrDefaultAsync();

            return response;
        }

        public async Task<OperationStatusResponse> CreateMotorVehicle(CreateMotorVehicleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Brand))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle brand." };

            if (string.IsNullOrWhiteSpace(request.Model))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle model." };

            if (request.InsurancePolicyId == 0)
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle insurance policy." };

            if (request.MotorVehicleTypeId == 0)
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle type." };

            if (request.SelectedDriversIds == null || request.SelectedDriversIds.Count == 0)
                return new OperationStatusResponse { IsSuccessful = false, Message = "You must assign at least one driver to the vehicle." };

            try
            {
                Entities.MotorVehicle motorvehicleDto = new() { Brand = request.Brand, Model = request.Model, MotorVehicleTypeId = request.MotorVehicleTypeId, InsurancePolicyId = request.InsurancePolicyId };

                _context.Add(motorvehicleDto);

                foreach (var id in request.SelectedDriversIds)
                {
                    Entities.MotorVehicleDriver motorVehicleDriverDto = new()
                    {
                        DriverId = id
                    };
                    motorvehicleDto.MotorVehicleDriver.Add(motorVehicleDriverDto);
                }
                await _context.SaveChangesAsync();

                return new OperationStatusResponse { IsSuccessful = true, Message = "Success. Motor vehicle created successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> UpdateMotorVehicle(UpdateMotorVehicleRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Brand))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle brand." };

            if (string.IsNullOrWhiteSpace(request.Model))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle model." };

            if (request.InsurancePolicyId == 0)
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle insurance policy." };

            if (request.MotorVehicleTypeId == 0)
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid motor vehicle type." };

            if (request.SelectedDriversIds == null || request.SelectedDriversIds.Count == 0)
                return new OperationStatusResponse { IsSuccessful = false, Message = "You must assign at least one driver to the vehicle." };

            try
            {
                var motorvehicleDto = await _context.MotorVehicle.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (motorvehicleDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Motor vehicle with ID {request.Id} not found." };

                motorvehicleDto.Brand = request.Brand;
                motorvehicleDto.Model = request.Model;
                motorvehicleDto.MotorVehicleTypeId = request.MotorVehicleTypeId;
                motorvehicleDto.InsurancePolicyId = request.InsurancePolicyId;

                var rowsFromDatabase = await _context.MotorVehicleDriver.Where(x => x.MotorVehicleId == request.Id).ToListAsync();

                var rowsToDelete = getRowsToDelete(request.SelectedDriversIds, rowsFromDatabase);
                var rowsToAdd = getRowsToAdd(request.SelectedDriversIds, rowsFromDatabase);

                if (rowsToDelete.Count > 0)
                {
                    foreach (MotorVehicleDriver row in rowsToDelete)
                    {
                        var item = rowsFromDatabase.Find(x => x.Id == row.Id);

                        if (item != null)
                            _context.Remove(item);
                    }
                }

                if (rowsToAdd.Count > 0)
                {
                    foreach (int id in rowsToAdd)
                    {
                        MotorVehicleDriver studentSubjectDTO = new()
                        {
                            MotorVehicleId = request.Id,
                            DriverId = id,
                        };
                        _context.Add(studentSubjectDTO);
                    }
                }

                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Motor vehicle with ID {motorvehicleDto.Id} updated successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        private List<MotorVehicleDriver> getRowsToDelete(List<int> request, List<MotorVehicleDriver> databaseItems)
        {
            List<MotorVehicleDriver> listToReturn = new();

            foreach (MotorVehicleDriver item in databaseItems)
            {

                if (!request.Any(id => id == item.DriverId))
                {
                    listToReturn.Add(item);
                }

            }

            return listToReturn;
        }

        private List<int> getRowsToAdd(List<int> request, List<MotorVehicleDriver> databaseItems)
        {
            List<int> listToReturn = new();

            foreach (int id in request)
            {
                if (!databaseItems.Any(x => x.DriverId == id))
                {
                    listToReturn.Add(id);
                }
            }

            return listToReturn;
        }

        public async Task<OperationStatusResponse> DeleteMotorVehicle(IdRequest request)
        {
            try
            {
                var motorvehicleDto = await _context.MotorVehicle.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (motorvehicleDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Motor vehicle with ID {request.Id} not found." };

                _context.Remove(motorvehicleDto);
                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Motor vehicle with ID {motorvehicleDto.Id} deleted successfully." };
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
