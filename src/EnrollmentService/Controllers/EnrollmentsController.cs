using EnrollmentService.Interfaces;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;
        private readonly ILogger<EnrollmentsController> _logger;

        public EnrollmentsController(IEnrollmentService service, ILogger<EnrollmentsController> logger)
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
            if (item == null) return NotFound(new { message = "Enrollment not found" });

            // enrich with student and course details
            var manager = _service as EnrollmentService.Services.EnrollmentManager;
            var student = await manager!.GetStudentDetailsAsync(item.StudentId);
            var course = await manager!.GetCourseDetailsAsync(item.CourseId);

            return Ok(new { enrollment = item, student, course });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Enrollment enrollment)
        {
            if (enrollment == null) return BadRequest(new { message = "Invalid enrollment" });
            var created = await _service.CreateAsync(enrollment);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound(new { message = "Enrollment not found" });
            return NoContent();
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var list = await _service.GetByStudentAsync(studentId);
            return Ok(list);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var list = await _service.GetByCourseAsync(courseId);
            return Ok(list);
        }
    }
}
