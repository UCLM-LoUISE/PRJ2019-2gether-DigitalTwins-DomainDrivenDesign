using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChildHDT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfWork;
        private readonly RepositoryChild _repo;

        public ChildController(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            _repo = new RepositoryChild(unitOfwork); 
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Child>> GetChild(Guid id)
        {
            var child = await _repo.FindById(id);
            return Ok(child);
        }

        [HttpPost]
        public async Task<ActionResult<Child>> PostChild(Child child)
        {
            var result = await _repo.Add(child);
            return result;
        }
    }
}
