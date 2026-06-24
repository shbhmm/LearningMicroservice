using Microsoft.AspNetCore.Mvc;
using StudentService.Interfaces;
using StudentService.Models;

namespace StudentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService service, ILogger<StudentsController> logger)
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
            if (item == null) return NotFound(new { message = "Student not found" });
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            if (student == null) return BadRequest(new { message = "Invalid student" });
            var created = await _service.CreateAsync(student);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            if (student == null) return BadRequest(new { message = "Invalid student" });
            var ok = await _service.UpdateAsync(id, student);
            if (!ok) return NotFound(new { message = "Student not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound(new { message = "Student not found" });
            return NoContent();
        }
    }
}
