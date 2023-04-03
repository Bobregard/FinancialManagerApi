using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Service.Interfaces
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank>> GetAllBanks();

        Task<Bank> GetBankById(int bankId);

        Task AddBank(Bank transaction);

        Task DeleteBank(Bank transaction);
    }
}
