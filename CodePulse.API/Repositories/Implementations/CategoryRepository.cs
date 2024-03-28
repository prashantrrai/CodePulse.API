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
            try
            {
                await dbcontext.Categories.AddAsync(category);
                await dbcontext.SaveChangesAsync();

                return category;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                var data = await dbcontext.Categories.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Category> GetAsyncById(Guid Id)
        {
            try
            {
                var data = await dbcontext.Categories.FindAsync(Id);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var category = await dbcontext.Categories.FindAsync(Id);
                if (category == null)
                {
                    return false;
                }

                dbcontext.Categories.Remove(category);
                await dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Category> UpdateAsync (Guid Id, Category category)
        {
            try
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
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
