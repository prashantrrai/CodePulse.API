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

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var data = await categoryRepository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(Guid Id)
        {
            var data = await categoryRepository.GetAsyncById(Id);
            if (data == null)
            {
                return NotFound(); // Return 404 Not Found if the category is not found
            }
            return Ok(data);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategory (Guid Id)
        {
            var result = await categoryRepository.DeleteAsync(Id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { Message = $"Category ID {Id} deleted successfully" });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategory (Guid Id, [FromBody] CreateCategoryRequestDto request)
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

    }
}
