using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Events;
using ChildHDT.Infrastructure.EventSourcing.Registries;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using ChildHDT.Infrastructure.IntegrationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly RoleAssignment _roleAssignment;
        private readonly IConfiguration _configuration;

        public ChildController(IUnitOfwork unitOfwork, IConfiguration configuration)
        {
            _unitOfWork = unitOfwork;
            _configuration = configuration;
            _repo = new RepositoryChild(unitOfwork, _configuration);
            _roleAssignment = new RoleAssignment(_repo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Child>> GetChild(Guid id)
        {
            var child = await _repo.FindById(id);
            if (child == null)
            {
                return NotFound();
            }
            return Ok(child);
        }

        [HttpPost]
        public async Task<ActionResult<Child>> PostChild(Child child)
        {
            var result = await _repo.Add(child);
            return result;
        }
        [HttpPut("{id}/Role/Victim")]
        public async Task<ActionResult<Child>> AssignVictimRoleToChild(Guid id)
        {
            var child = await _roleAssignment.AssignRoleVictimToChild(id);
            return child;
        }

        [HttpPut("{id}/Role/Bully")]
        public async Task<ActionResult<Child>> AssignBullyRoleToChild(Guid id)
        {
            var child = await _roleAssignment.AssignRoleBullyToChild(id);
            return child;
        }

        [HttpPut("{id}/Role/ToMObserver")]
        public async Task<ActionResult<Child>> AssignToMObserverRoleToChild(Guid id)
        {
            var child = await _roleAssignment.AssignRoleToMObserverToChild(id);
            return child;
        }

        [HttpPut("{id}/Role/NonToMObserver")]
        public async Task<ActionResult<Child>> AssignNonToMObserverRoleToChild(Guid id)
        {
            var child = await _roleAssignment.AssignRoleNonToMObserverToChild(id);
            return child;
        }

        [HttpPut("{id}/Role/Delete")]
        public async Task<ActionResult<Child>> DeleteRoleFromChild(Guid id)
        {
            var child = await _roleAssignment.DeleteRoleToChild(id);
            return child;
        }

        [HttpGet("{id}/Stress/{from}/{to}")]
        public async Task<ActionResult<StressEvent>> GetStressEvents(Guid id, DateTime from, DateTime to)
        {
            var child = await _repo.FindById(id);
            if (child == null)
            {
                return NotFound();
            }
            
            var list = (child.Features as PWAFeatures).StressRegistry.GetEventsBetweenDates(from, to);
            return Ok(list);
        }

        [HttpGet("{id}/Speed/{from}/{to}")]
        public async Task<ActionResult<StressEvent>> GetSpeedEvents(Guid id, DateTime from, DateTime to)
        {
            var child = await _repo.FindById(id);
            if (child == null)
            {
                return NotFound();
            }

            var list = (child.Features as PWAFeatures).SpeedRegistry.GetEventsBetweenDates(from, to);
            return Ok(list);
        }

        [HttpGet("{id}/Location/{from}/{to}")]
        public async Task<ActionResult<StressEvent>> GetLocationEvents(Guid id, DateTime from, DateTime to)
        {
            var child = await _repo.FindById(id);
            if (child == null)
            {
                return NotFound();
            }

            var list = (child.Features as PWAFeatures).LocationRegistry.GetEventsBetweenDates(from, to);
            return Ok(list);
        }

        [HttpGet("{id}/Orientation/{from}/{to}")]
        public async Task<ActionResult<StressEvent>> GetOrientationEvents(Guid id, DateTime from, DateTime to)
        {
            var child = await _repo.FindById(id);
            if (child == null)
            {
                return NotFound();
            }

            var list = (child.Features as PWAFeatures).OrientationRegistry.GetEventsBetweenDates(from, to);
            return Ok(list);
        }
    }
}
