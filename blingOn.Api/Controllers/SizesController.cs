using blingOn.Application;
using blingOn.Application.DTOs;
using blingOn.Implementation.Commands;
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
    public class SizesController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public SizesController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // POST api/<SizesController>
        [HttpPost]
        public IActionResult Post([FromBody] SizeDto dto,
            [FromServices] CreateSizeCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Successfully added a size.");
        }

        // DELETE api/<SizesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] DeleteSizeCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
