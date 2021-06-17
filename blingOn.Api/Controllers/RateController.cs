using blingOn.Application;
using blingOn.Application.DTOs;
using blingOn.Application.Interfaces;
using blingOn.DataAccess;
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
    public class RateController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        private readonly IApplicationActor _actor;
        private readonly BlingOnContext _context;

        public RateController(UseCaseExecutor executor, IApplicationActor actor, BlingOnContext context)
        {
            _executor = executor;
            _actor = actor;
            _context = context;
        }

        // POST api/<RateController>
        [HttpPost]
        public IActionResult Post([FromBody] RateDto dto,
            [FromServices] RateCommand command)
        {
            var user = _context.Ratings.FirstOrDefault(x => x.UserId == _actor.Id && x.ProductId == dto.ProductId);

            if(user != null)
            {
                return UnprocessableEntity("You already rated this product.");
            }

            _executor.ExecuteCommand(command, dto);
            return StatusCode(201, "Successfully rated a product!");
        }

       
    }
}
