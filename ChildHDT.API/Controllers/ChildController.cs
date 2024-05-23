using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
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

        public ChildController(IUnitOfwork unitOfwork, IOptions<MQTTSettings> mqttSettings, IOptions<RabbitMQSettings> rabbitMQSettings, IOptions<PostgreSQLSettings> postgreSQLSettings)
        {
            _unitOfWork = unitOfwork;
            _repo = new RepositoryChild(unitOfwork);
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
