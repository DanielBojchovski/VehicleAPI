using Azure;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.Common.Models;
using VehicleAPI.Common.Requests;
using VehicleAPI.Common.Responses;
using VehicleAPI.Entities;
using VehicleAPI.Repositories.InsurancePolicy.Interfaces;
using VehicleAPI.Repositories.InsurancePolicy.Models;
using VehicleAPI.Repositories.InsurancePolicy.Requests;
using VehicleAPI.Repositories.InsurancePolicy.Responses;

namespace VehicleAPI.Repositories.InsurancePolicy.Services
{
    public class InsurancePolicyService : IInsurancePolicyService
    {
        private readonly VehicleDBContext _context;

        public InsurancePolicyService(VehicleDBContext context)
        {
            _context = context;
        }

        public async Task<GetAllInsurancePoliciesResponse> GetAllInsurancePolicies()
        {
            var list = await _context.InsurancePolicy
                    .AsNoTracking()
                    .Select(x => new InsurancePolicyModel
                    {
                        Id = x.Id,
                        PolicyProvider = x.PolicyProvider
                    }).ToListAsync();

            return new GetAllInsurancePoliciesResponse { List = list };
        }

        public async Task<InsurancePolicyModel?> GetInsurancePolicyById(IdRequest request)
        {
            var response = await _context.InsurancePolicy
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .Select(x => new InsurancePolicyModel
                {
                    Id = x.Id,
                    PolicyProvider = x.PolicyProvider
                }).FirstOrDefaultAsync();

            return response;
        }

        public async Task<OperationStatusResponse> CreateInsurancePolicy(CreateInsurancePolicyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PolicyProvider))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid policy provider name." };

            try
            {
                VehicleAPI.Entities.InsurancePolicy insurencePolicyDto = new() { PolicyProvider = request.PolicyProvider };

                _context.Add(insurencePolicyDto);
                await _context.SaveChangesAsync();

                return new OperationStatusResponse { IsSuccessful = true, Message = "Success. Insurance policy created successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> UpdateInsurancePolicy(UpdateInsurancePolicyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PolicyProvider))
                return new OperationStatusResponse { IsSuccessful = false, Message = "Invalid policy provider name." };

            try
            {
                var insurencePolicyDto = await _context.InsurancePolicy.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (insurencePolicyDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Insurance policy with ID {request.Id} not found." };

                insurencePolicyDto.PolicyProvider = request.PolicyProvider;
                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Insurance policy with ID {insurencePolicyDto.Id} updated successfully." };
            }
            catch (Exception ex)
            {
                return new OperationStatusResponse { IsSuccessful = false, Message = $"An error occurred: {ex.Message}" };
            }
        }

        public async Task<OperationStatusResponse> DeleteInsurancePolicy(IdRequest request)
        {
            try
            {
                var insurencePolicyDto = await _context.InsurancePolicy.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

                if (insurencePolicyDto is null)
                    return new OperationStatusResponse { IsSuccessful = false, Message = $"Insurance policy with ID {request.Id} not found." };

                _context.Remove(insurencePolicyDto);
                await _context.SaveChangesAsync();
                return new OperationStatusResponse { IsSuccessful = true, Message = $"Success. Insurance policy with ID {insurencePolicyDto.Id} deleted successfully." };
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

        public async Task<GetInsurancePoliciesForMotorVehicleResponse> GetInsurancePoliciesForMotorVehicleCreate()
        {
            var list = await _context.InsurancePolicy
                    .Where(x => !_context.MotorVehicle.Any(y => y.InsurancePolicyId == x.Id))
                    .AsNoTracking()
                    .Select(x => new InsurancePolicyModel
                    {
                        Id = x.Id,
                        PolicyProvider = x.PolicyProvider
                    }).ToListAsync();

            return new GetInsurancePoliciesForMotorVehicleResponse { List = list };
        }

        public async Task<GetInsurancePoliciesForMotorVehicleResponse> GetInsurancePoliciesForMotorVehicleUpdate(IdRequest request)
        {
            var list = await _context.InsurancePolicy
                    .Where(x => !_context.MotorVehicle.Any(y => y.InsurancePolicyId == x.Id))
                    .AsNoTracking()
                    .Select(x => new InsurancePolicyModel
                    {
                        Id = x.Id,
                        PolicyProvider = x.PolicyProvider
                    }).ToListAsync();

            var currentPolicy = await _context.MotorVehicle
                    .Where(x => x.Id == request.Id)
                    .AsNoTracking()
                    .Select(x => new InsurancePolicyModel
                    {
                        Id = x.InsurancePolicyId,
                        PolicyProvider = x.InsurancePolicy.PolicyProvider
                    }).ToListAsync();

            return new GetInsurancePoliciesForMotorVehicleResponse { List = [..list, ..currentPolicy] };
        }
    }
}