using Ecommerce_API.Ropsitory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = categoryRepository.GetAll();
            if(categoryList != null)
            {
                return Ok(categoryList);
            }
            return BadRequest();
        }
    }
}
