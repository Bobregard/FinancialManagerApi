using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Service.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();

        Task<Category> GetCategoryById(int categoryId);

        Task AddCategory(Category category);

        Task DeleteCategory(Category category);
    }
}
