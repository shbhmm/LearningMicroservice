using CourseService.Interfaces;
using CourseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseService service, ILogger<CoursesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(new { message = "Course not found" });
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            if (course == null) return BadRequest(new { message = "Invalid course" });
            var created = await _service.CreateAsync(course);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course course)
        {
            if (course == null) return BadRequest(new { message = "Invalid course" });
            var ok = await _service.UpdateAsync(id, course);
            if (!ok) return NotFound(new { message = "Course not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound(new { message = "Course not found" });
            return NoContent();
        }
    }
}
