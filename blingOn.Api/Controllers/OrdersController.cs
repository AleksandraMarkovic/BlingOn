using blingOn.Application;
using blingOn.Application.DTOs;
using blingOn.Application.Exceptions;
using blingOn.Application.Interfaces;
using blingOn.Application.Searches;
using blingOn.DataAccess;
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
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly BlingOnContext _context;
        private readonly IApplicationActor _actor;

        public OrdersController(UseCaseExecutor executor, BlingOnContext context, IApplicationActor actor)
        {
            _executor = executor;
            _context = context;
            _actor = actor;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearch search,
            [FromServices] GetOrdersQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto,
            [FromServices] CreateOrderCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Successfully placed the order!");
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateOrderDto dto,
            [FromServices] UpdateOrderCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] DeleteOrderCommand command)
        {
            var order = _context.Orders.Find(id);
            if (order.UserId != _actor.Id)
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
