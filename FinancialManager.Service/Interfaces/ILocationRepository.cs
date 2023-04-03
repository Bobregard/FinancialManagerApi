using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Service.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();

        Task<Location> GetLocationById(int locationId);

        Task AddLocation(Location location);

        Task DeleteLocation(Location location);
    }
}
