using VehicleAPI.Repositories.Driver.Models;

namespace VehicleAPI.Repositories.Driver.Responses
{
    public class GetAllDriversResponse
    {
        public List<DriverModel> List { get; set; } = new();
    }
}
