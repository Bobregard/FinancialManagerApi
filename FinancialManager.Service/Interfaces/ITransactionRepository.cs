using FinancialManager.Core.DTOs;
using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Service.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();

        Task<Transaction> GetTransactionById(int transactionId);

        Task AddTransaction(Transaction transaction);

        Task DeleteTransaction(Transaction transaction);

        Task<IEnumerable<Transaction>> GetTransactionsByLocation(int locationId);
        Task<IEnumerable<Transaction>> GetTransactionsAcrossABank(int bankId);
        Task<IEnumerable<Transaction>> GetTransactionsOfUser(string userId);
        Task<IEnumerable<Transaction>> GetTransactionsByCategory(int categoryId);
        Task<IEnumerable<Transaction>> GetTransactionsByType(string type);
        Task<IEnumerable<Transaction>> GetTransactionsOverAPeriodOfTime(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TransactionSummary>> GetMonthlyTransactionsByCategoryAndBank(int month);
    }
}
