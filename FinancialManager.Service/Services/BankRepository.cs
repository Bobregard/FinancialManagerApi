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
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(AppDbContext db) : base(db)
        {
        }

        public async Task AddBank(Bank bank)
        {
            await AddAsync(bank);
        }

        public async Task DeleteBank(Bank bank)
        {
            await DeleteAsync(bank);
        }

        public async Task<IEnumerable<Bank>> GetAllBanks()
        {
            var result = await GetAllAsync().Result.ToListAsync();
            return result;
        }

        public async Task<Bank> GetBankById(int bankId)
        {
            return await GetFirstOrDefaultAsync(b => b.Id == bankId);
        }
    }
}
