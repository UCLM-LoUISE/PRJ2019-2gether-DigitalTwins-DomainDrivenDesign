﻿using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.Entities;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Events;
using ChildHDT.Infrastructure.EventSourcing.Registries;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using ChildHDT.Infrastructure.IntegrationServices;
using ChildHDT.Infrastructure.Settings;
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
        private readonly IOptions<MQTTSettings> _mqttSettings;
        private readonly IOptions<RabbitMQSettings> _rabbitmqSettings;
        private readonly IOptions<PostgreSQLSettings> _postgreSQLSettings;
        private readonly RoleAssignment _roleAssignment;

        public ChildController(IUnitOfwork unitOfwork, IOptions<MQTTSettings> mqttSettings, IOptions<RabbitMQSettings> rabbitMQSettings, IOptions<PostgreSQLSettings> postgreSQLSettings)
        {
            _unitOfWork = unitOfwork;
            _repo = new RepositoryChild(unitOfwork);
            _roleAssignment = new RoleAssignment(_repo);
            _mqttSettings = mqttSettings;
            _rabbitmqSettings = rabbitMQSettings;
            _postgreSQLSettings = postgreSQLSettings;
        }

        //public IActionResult About()
        //{
        //    ViewData["Hostname"] = _rabbitmqSettings.Value.Hostname;
        //    ViewData["ConnectionString"] = _postgreSQLSettings.Value.ConnectionString;
        //    ViewData["Server"] = _mqttSettings.Value.Server;
        //}

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
            
            var list = (child.Features as PWAFeatures).SpeedRegistry.GetEventsBetweenDates(from, to);
            return Ok(list);
        }
    }
}
