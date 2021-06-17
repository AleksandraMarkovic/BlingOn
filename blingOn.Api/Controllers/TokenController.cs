using blingOn.Api.Core;
using blingOn.Application.Exceptions;
using blingOn.DataAccess;
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
    public class TokenController : ControllerBase
    {
        private readonly JwtManager _manager;
        private readonly BlingOnContext _context;

        public TokenController(JwtManager manager, BlingOnContext context)
        {
            _manager = manager;
            _context = context;
        }

        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password
            && x.DeletedAt == null);

                if (user == null)
                {
                    return UnprocessableEntity("User not found");
                }


                var token = _manager.MakeToken(request.Email, request.Password);

                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(new
                {
                    token
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "There has been an error.");
            }
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
