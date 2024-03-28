using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var category = new Category
                {
                    Name = request.Name,
                    UrlHandle = request.UrlHandle,
                };

                await categoryRepository.CreateAsync(category);

                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var data = await categoryRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(Guid Id)
        {
            try
            {
                var data = await categoryRepository.GetAsyncById(Id);
                if (data == null)
                {
                    return NotFound(); // Return 404 Not Found if the category is not found
                }

                return Ok(data);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory (Guid Id)
        {
            try
            {
                var result = await categoryRepository.DeleteAsync(Id);
                if (!result)
                {
                    return NotFound();
                }

                return Ok(new { Message = $"Category ID {Id} deleted successfully" });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategory (Guid Id, [FromBody] CreateCategoryRequestDto request)
        {
            try
            {
                // Map DTO to Domain Model
                var category = new Category
                {
                    Name = request.Name,
                    UrlHandle = request.UrlHandle,
                };

                var result = await categoryRepository.UpdateAsync(Id, category);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
