using CodeBlog.API.Models.Domain;

namespace CodeBlog.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetById(Guid id);
        Task<Category?> UpdateCategoryAsync(Category category);
        Task<Category?> DeleteCategoryAsync(Guid id);
    }
}
