using VehicleAPI.Repositories.Driver.Interfaces;
using VehicleAPI.Repositories.Driver.Services;
using VehicleAPI.Repositories.InsurancePolicy.Interfaces;
using VehicleAPI.Repositories.InsurancePolicy.Services;
using VehicleAPI.Repositories.MotorVehicle.Interfaces;
using VehicleAPI.Repositories.MotorVehicle.Services;
using VehicleAPI.Repositories.MotorVehicleType.Interfaces;
using VehicleAPI.Repositories.MotorVehicleType.Services;

namespace VehicleAPI.AppStartUp
{
    public static class DependencyInjectionBuilder
    {
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<IMotorVehicleTypeService, MotorVehicleTypeService>();

            services.AddScoped<IInsurancePolicyService, InsurancePolicyService>();

            services.AddScoped<IDriverService, DriverService>();

            services.AddScoped<IMotorVehicleService, MotorVehicleService>();

            return services;
        }
    }
}
