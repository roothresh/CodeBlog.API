using CodeBlog.API.Data;
using CodeBlog.API.Models.Domain;
using CodeBlog.API.Models.DTO;
using CodeBlog.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.API.Controllers
{
    // https://localhost:xxxx/api/categories olacak
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        //DI yaptık
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ICategoryRepository CategoryRepository { get; }

        // From body request body de geliyor demek
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO request)
        {
            // MAP DTO to domain model 
            var category = new Category { Name = request.Name, UrlHandle = request.UrlHandle };

            await categoryRepository.CreateAsync(category);

            // Domain model to DTO
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };


            return Ok(response);
        }
        //path GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllCategoriesAsync();

            var response = new List<CategoryDTO>();

            //map domain model to dto for not expose or domain
            foreach (var category in categories)
            {
                response.Add(new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                });
            }

            return Ok(response);
        }

        //fromRoute request url den geliyor demek
        // GET: /api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var exisitingCategory = await categoryRepository.GetById(id);

            if (exisitingCategory == null)
                return NotFound();

            var response = new CategoryDTO { Id = exisitingCategory.Id, Name = exisitingCategory.Name, UrlHandle = exisitingCategory.UrlHandle };

            return Ok(response);
        }
        //put: /api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody]UpdateCategoryRequestDto request)
        {
            //conver dto to model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
            category = await categoryRepository.UpdateCategoryAsync(category);

            if(category == null)
            {
                return NotFound();
            }

            //dto yu model e çevir
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteCategoryAsync(id);

            if(category is null)
            {
                return NotFound();
            }

            //dto to model yapalım
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);

        }
    }
}
