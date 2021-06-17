using blingOn.Application;
using blingOn.Application.Searches;
using blingOn.DataAccess;
using blingOn.Implementation.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blingOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UseCaseLogsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UseCaseLogsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UseCaseLogsController>
        [HttpGet]
        public IActionResult Get([FromQuery] LogSearch search,
            [FromServices] GetLogsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }
    }
}
