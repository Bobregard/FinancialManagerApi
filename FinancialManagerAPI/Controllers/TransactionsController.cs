using AutoMapper;
using FinancialManager.Controllers;
using FinancialManager.Core.DTOs;
using FinancialManager.Interfaces;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagerAPI.Controllers
{
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController : BaseApiController
    {
        private readonly UserManager<User> _userManager;

        public TransactionsController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper, UserManager<User> userManager) 
            : base(unitOfWork, logger, mapper)
        {
            _userManager = userManager;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTransactions() 
        {          
            try
            {
                var transactions = await _unitOfWork.TransactionRepository.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllTransactions)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getById/{transactionId}", Name = "GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetTransactionById(transactionId);
            if (transaction is not null)
            {
                var transactionReturn = _mapper.Map<TransactionDto>(transaction);
                return Ok(transactionReturn);
            }
            _logger.LogInfo($"Transaction with id: {transactionId} doesn't exist in the database.");
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDto incomingTransaction)
        {
            var transaction = _mapper.Map<Transaction>(incomingTransaction);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            
            transaction.Date = DateTime.UtcNow;
            transaction.UserId = user.Id;
            await _unitOfWork.TransactionRepository.AddTransaction(transaction);
            await _unitOfWork.SaveAsync();
            var transactionReturn = _mapper.Map<TransactionDto>(transaction);
            return CreatedAtRoute("GetTransactionById", new { transactionId = transactionReturn.Id }, transactionReturn);
        }

        [HttpDelete("delete/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetTransactionById(transactionId);
            if (transaction is null)
            {
                return NotFound();
            }
            await _unitOfWork.TransactionRepository.DeleteTransaction(transaction);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpGet("getFromLocation/{locationId}")]
        public async Task<IActionResult> GetTransactionsByLocation(int locationId)
        {
            var result = await _unitOfWork.TransactionRepository.GetTransactionsByLocation(locationId);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions occured within the specified location.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getFromBank/{bankId}")]
        public async Task<IActionResult> GetTransactionsAcrossABank(int bankId)
        {
            var result = await _unitOfWork.TransactionRepository.GetTransactionsAcrossABank(bankId);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions occured within the specified bank.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getFromUser/{userId}")]
        public async Task<IActionResult> GetTransactionsOfUser(string userId)
        {
            var result = await _unitOfWork.TransactionRepository.GetTransactionsOfUser(userId);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions by specified user.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getFromCategory/{categoryId}")]
        public async Task<IActionResult> GetTransactionsByCategory(int categoryId)
        {
            var result = await _unitOfWork.TransactionRepository.GetTransactionsByCategory(categoryId);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions from specified category.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getFromType/{type}")]
        public async Task<IActionResult> GetTransactionsByType(string type)
        {
            var result = await _unitOfWork.TransactionRepository.GetTransactionsByType(type);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions from specified type.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getForPeriod/{startDate}/{endDate}")]
        public async Task<IActionResult> GetTransactionsOverAPeriodOfTime(DateTime startDate, DateTime endDate)
        {
            var result = await _unitOfWork.TransactionRepository.GetTransactionsOverAPeriodOfTime(startDate, endDate);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions from specified range.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("getByMonth/{month}")]
        public async Task<IActionResult> GetMonthlyTransactionsByCategoryAndBank(int month)
        {
            var result = await _unitOfWork.TransactionRepository.GetMonthlyTransactionsByCategoryAndBank(month);
            if (!result.Any())
            {
                _logger.LogInfo($"No Transactions from specified month.");
                return NotFound();
            }
            return Ok(result);
        }
    }
}
