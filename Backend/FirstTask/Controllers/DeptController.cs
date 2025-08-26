using CoreLayer.Dtos;
using CoreLayer.Interfaces;
using CoreLayer.Models;
using EntityLayer.AppDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DeptController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityBaseRepository<Dept> entityBaseRepository;

        public DeptController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get")]
        public async Task<IActionResult> getAllDeptAsync(int pageSize = 0, int pageNumber = 0, bool ascending = true)
        {
            IQueryable<Dept> query = _unitOfWork.CreateRepository<Dept>().getQueryable();
            var totalCount = await query.CountAsync();

            // Apply sorting before pagination
            query = ascending ? query.OrderBy(d => d.Name) : query.OrderByDescending(d => d.Name);

            // Apply pagination if requested
            if (pageSize > 0 && pageNumber > 0)
            {
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            var items = await query.ToListAsync();
            return Ok(new { items, totalCount });
        }
        [HttpGet("getCount")]                  
        public async Task<IActionResult> getDeptCountAsync()
        {
            var count = await _unitOfWork.CreateRepository<Dept>().getQueryable().CountAsync();
            return Ok(count);
        }
        [HttpGet("search")]
        public async Task<IActionResult> searchDeptsByName(string searchQuery)
        {
            var deptsFound = await _unitOfWork.CreateRepository<Dept>().getQueryable()
                .Where(d => d.Name.Contains(searchQuery))
                .ToListAsync();
            return Ok(deptsFound);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateDeptAsync(int id, [FromBody] DeptDto deptDto)
        {
            var dept = await _unitOfWork.CreateRepository<Dept>().getQueryable().FirstOrDefaultAsync(d => d.Id == id);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            dept.Name = deptDto.Name;
            _unitOfWork.CreateRepository<Dept>().update(dept);

            await _unitOfWork.CompleteAsync();
            return Ok(dept);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteDeptAsync(int id)
        {
            var deptRepository = _unitOfWork.CreateRepository<Dept>();
            var dept = await deptRepository.getByIdAsync(id);
            if (dept == null)
            {
                return NotFound("Department not found.");
            }
            try {
                deptRepository.remove(dept);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Cannot delete department as it is referenced by other entities.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> addDeptAsync([FromBody] DeptDto deptDto)
        {
            if (deptDto == null)
            {
                return BadRequest("Invalid department data.");
            }
            if (string.IsNullOrWhiteSpace(deptDto.Name))
            {
                return BadRequest("Department name cannot be empty.");
            }
            var dept = new Dept
            {
                Name = deptDto.Name
            };
            _unitOfWork.CreateRepository<Dept>().add(dept);
            await _unitOfWork.CompleteAsync();
            return Ok(dept);
        }
    }
}
