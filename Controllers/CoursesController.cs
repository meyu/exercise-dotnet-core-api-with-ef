using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using exercise_dotnet_core_api_with_ef.Models;

namespace exercise_dotnet_core_api_with_ef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public CoursesController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Course>> GetCourse(long id)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // GET: api/Courses/Student
        [HttpGet("Student")]
        public async Task<ActionResult<IEnumerable<VwCourseStudents>>> GetCourseStudent()
        {
            return await _context.VwCourseStudents.ToListAsync();
        }

        // GET: api/Courses/5/Student
        [HttpGet("{id:int}/Student")]
        public async Task<ActionResult<IEnumerable<VwCourseStudents>>> GetCourseStudent(long id)
        {
            var r = await (
                from a in _context.VwCourseStudents
                where a.CourseId == id
                select a
            ).ToListAsync();

            if (r == null)
            {
                return NotFound();
            }
            return r;
        }

        // GET: api/Courses/StudentCount
        [HttpGet("StudentCount")]
        public async Task<ActionResult<IEnumerable<VwCourseStudentCount>>> GetCourseStudentCount()
        {
            return await _context.VwCourseStudentCount.ToListAsync();
        }

        // GET: api/Courses/5/StudentCount
        [HttpGet("{id:int}/StudentCount")]
        public async Task<ActionResult<VwCourseStudentCount>> GetCourseStudentCount(long id)
        {
            var r = await (
                from a in _context.VwCourseStudentCount
                where a.CourseId == id
                select a
            ).SingleAsync();

            if (r == null)
            {
                return NotFound();
            }
            return r;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(long id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(course.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(long id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(long id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }
}
