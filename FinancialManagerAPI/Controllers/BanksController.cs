using AutoMapper;
using FinancialManager.Controllers;
using FinancialManager.Core.DTOs;
using FinancialManager.Interfaces;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagerAPI.Controllers
{
    [Route("api/banks")]
    [Authorize]
    public class BanksController : BaseApiController
    {
        public BanksController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper) 
            : base(unitOfWork, logger, mapper)
        {
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBanks()
        {
            try
            {
                var banks = await _unitOfWork.BankRepository.GetAllBanks();
                return Ok(banks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllBanks)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getById/{bankId}", Name = "GetBankById")]
        public async Task<IActionResult> GetBankById(int bankId)
        {
            var bank = await _unitOfWork.BankRepository.GetBankById(bankId);
            if (bank is not null)
            {
                return Ok(bank);
            }
            _logger.LogInfo($"Bank with id: {bankId} doesn't exist in the database.");
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBank([FromBody] BankCreateDto incomingBank)
        {
            var bank = _mapper.Map<Bank>(incomingBank);
            await _unitOfWork.BankRepository.AddBank(bank);
            await _unitOfWork.SaveAsync();
            var bankReturn = _mapper.Map<BankDto>(bank);
            return CreatedAtRoute("GetBankById", new { bankId = bankReturn.Id }, bankReturn);
        }

        [HttpDelete("delete/{bankId}")]
        public async Task<IActionResult> DeleteBank(int bankId)
        {
            var bank = await _unitOfWork.BankRepository.GetBankById(bankId);
            if (bank is null)
            {
                return NotFound();
            }
            await _unitOfWork.BankRepository.DeleteBank(bank);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
