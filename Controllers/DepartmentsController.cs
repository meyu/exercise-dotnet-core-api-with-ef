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
    public class DepartmentsController : ControllerBase
    {
        private readonly ContosoUniversityContext _context;

        public DepartmentsController(ContosoUniversityContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await _context.Department.Where(d => d.IsDeleted == false).ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetDepartment(long id)
        {
            var department = await _context.Department.FirstOrDefaultAsync(d => d.IsDeleted == false && d.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // GET: api/Departments/CourseCount/Raw
        [HttpGet("CourseCount/Raw")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCountRaw()
        {
            return await _context.VwDepartmentCourseCount.ToListAsync();
        }

        // GET: api/Departments/CourseCount
        [HttpGet("CourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCount()
        {
            return await _context.VwDepartmentCourseCount
            .FromSqlRaw("SELECT * FROM VwDepartmentCourseCount")
            .ToListAsync();
        }

        // GET: api/Departments/5/CourseCount
        [HttpGet("{id:int}/CourseCount")]
        public async Task<ActionResult<VwDepartmentCourseCount>> GetDepartmentCourseCount(long id)
        {
            var DepartmentId = id;
            var r = await _context.VwDepartmentCourseCount
            .FromSqlInterpolated($"SELECT * FROM VwDepartmentCourseCount WHERE DepartmentId = {DepartmentId}")
            .SingleAsync();

            if (r == null)
            {
                return NotFound();
            }

            return r;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(long id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.Department.Add(department);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartmentExists(department.DepartmentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(long id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            department.IsDeleted = true;
            _context.Department.Update(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(long id)
        {
            return _context.Department.Any(e => e.DepartmentId == id);
        }
    }
}
