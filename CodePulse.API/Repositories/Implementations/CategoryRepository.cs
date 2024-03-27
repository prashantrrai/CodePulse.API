using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodePulse.API.Repositories.Implementations
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext dbcontext;

        public CategoryRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }



        public async Task<Category> CreateAsync(Category category)
        {
            await dbcontext.Categories.AddAsync(category);
            await dbcontext.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var data = await dbcontext.Categories.ToListAsync();
            return data;
        }

        public async Task<Category> GetAsyncById(Guid Id)
        {
            var data = await dbcontext.Categories.FindAsync(Id);
            return data;
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            var category = await dbcontext.Categories.FindAsync(Id);
            if(category == null)
            {
                return false;
            }

            dbcontext.Categories.Remove(category);
            await dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Category> UpdateAsync (Guid Id, Category category)
        {
            var existingCategory = await dbcontext.Categories.FindAsync(Id);

            if (existingCategory == null)
            {
                return existingCategory;
            }

            existingCategory.Name = category.Name;
            existingCategory.UrlHandle = category.UrlHandle;

            await dbcontext.SaveChangesAsync();

            return existingCategory;
        }

    }
}
