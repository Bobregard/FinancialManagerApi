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
    [Route("api/categories")]
    [Authorize]
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
            : base(unitOfWork, logger, mapper)
        {
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllCategories)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getById/{categoryId}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _unitOfWork.LocationRepository.GetLocationById(categoryId);
            if (category is not null)
            {
                return Ok(category);
            }
            _logger.LogInfo($"Category with id: {categoryId} doesn't exist in the database.");
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto incomingCategory)
        {
            var category = _mapper.Map<Category>(incomingCategory);
            await _unitOfWork.CategoryRepository.AddCategory(category);
            await _unitOfWork.SaveAsync();
            var categoryReturn = _mapper.Map<CategoryDto>(category);
            return CreatedAtRoute("GetCategoryById", new { categoryId = categoryReturn.Id }, categoryReturn);
        }

        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryById(categoryId);
            if (category is null)
            {
                return NotFound();
            }
            await _unitOfWork.CategoryRepository.DeleteCategory(category);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
