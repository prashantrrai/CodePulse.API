using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync (Category category);
        Task<List<Category>> GetAllAsync ();
        Task<Category> GetAsyncById (Guid Id);
        Task<bool> DeleteAsync (Guid Id);
        Task<Category> UpdateAsync (Guid Id, Category category);
    }
}
