using CoreLayer.Models;
using CoreLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GroupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> getGroups() {
            var groups = await _unitOfWork.CreateRepository<Group>().getQueryable().Select(g => new { g.Id, g.Name }).ToListAsync();
            return Ok(groups);
        }
    }
}
