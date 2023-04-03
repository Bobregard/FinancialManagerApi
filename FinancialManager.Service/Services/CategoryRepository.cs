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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db) : base(db)
        {
        }

        public async Task AddCategory(Category category)
        {
            await AddAsync(category);
        }

        public async Task DeleteCategory(Category category)
        {
            await DeleteAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var result = await GetAllAsync().Result.ToListAsync();
            return result;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await GetFirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
