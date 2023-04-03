using FinancialManager.Core.DTOs;
using FinancialManager.DataAccess.Data;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Service.Services
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            var result =  await GetAllAsync().Result.ToListAsync();
            return result;
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            return await GetFirstOrDefaultAsync(t => t.Id == transactionId);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await AddAsync(transaction);
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            await DeleteAsync(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByLocation(int locationId)
        {
            var query = from location in _db.Locations
                        join transaction in _db.Transactions
                        on location.Id equals transaction.LocationId
                        where location.Id == locationId
                        select transaction;

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAcrossABank(int bankId)
        {
            var query = from bank in _db.Banks
                        join transaction in _db.Transactions
                        on bank.Id equals transaction.BankId
                        where bank.Id == bankId
                        select transaction;

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsOfUser(string userId)
        {
            var query = from user in _db.Users
                        join transaction in _db.Transactions
                        on user.Id equals transaction.UserId
                        where user.Id == userId
                        select transaction;

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategory(int categoryId)
        {
            var query = from category in _db.Categories
                        join transaction in _db.Transactions
                        on category.Id equals transaction.CategoryId
                        where category.Id == categoryId
                        select transaction;

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByType(string type)
        {
            var query = from transaction in _db.Transactions
                        where transaction.Type == type
                        select transaction;

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsOverAPeriodOfTime(DateTime startDate, DateTime endDate)
        {
            var query = from transaction in _db.Transactions
                        where transaction.Date >= startDate && transaction.Date <= endDate
                        select transaction;

            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TransactionSummary>> GetMonthlyTransactionsByCategoryAndBank(int month)
        {
            var query = from transaction in _db.Transactions
                        join bank in _db.Banks
                        on transaction.BankId equals bank.Id
                        join category in _db.Categories
                        on transaction.CategoryId equals category.Id
                        where transaction.Date.Month == month
                        group transaction by new
                        {
                            BankName = bank.Name,
                            CategoryName = category.Name,
                            transaction.BankId,
                            transaction.CategoryId
                        } into gt
                        orderby gt.Key.BankName
                        select new TransactionSummary
                        {
                            BankName = gt.Key.BankName,
                            CategoryName = gt.Key.CategoryName,
                            TotalTransactions = gt.Count(),
                            TotalAmount = gt.Sum(t => t.Amount)
                        };
            var result = await query.ToListAsync();
            return result;
        }
    }
}
