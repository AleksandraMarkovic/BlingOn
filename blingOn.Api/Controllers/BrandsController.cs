using blingOn.Application;
using blingOn.Application.DTOs;
using blingOn.Application.Searches;
using blingOn.Implementation.Commands;
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
    public class BrandsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public BrandsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<BrandsController>
        [HttpGet]
        public IActionResult Get([FromQuery] BrandSearch search,
            [FromServices] GetBrandsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<BrandsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] BrandDto dto,
            [FromServices] CreateBrandCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Successfully added a brand!");
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] BrandDto dto,
            [FromServices] UpdateBrandCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] DeleteBrandCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
