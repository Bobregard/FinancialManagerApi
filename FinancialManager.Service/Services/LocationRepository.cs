using FinancialManager.DataAccess.Data;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Service.Services
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(AppDbContext db) : base(db)
        {
        }

        public async Task AddLocation(Location location)
        {
            await AddAsync(location);
        }

        public async Task DeleteLocation(Location location)
        {
            await DeleteAsync(location);
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            var result = await GetAllAsync().Result.ToListAsync();
            return result;
        }

        public async Task<Location> GetLocationById(int locationId)
        {
            return await GetFirstOrDefaultAsync(l => l.Id == locationId);
        }
    }
}
