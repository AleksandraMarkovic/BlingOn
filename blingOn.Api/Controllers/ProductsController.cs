using blingOn.Application;
using blingOn.Application.DTOs;
using blingOn.Application.Interfaces;
using blingOn.Application.Searches;
using blingOn.Implementation;
using blingOn.Implementation.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blingOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public ProductsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearch search,
            [FromServices] GetProductsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromForm] ProductDto dto,
            [FromServices] CreateProductCommand command)
        {
            _executor.ExecuteCommand(command, dto);

            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(dto.ImagePath.FileName);
            var newImage = guid + extension;
            var path = Path.Combine("wwwroot", "images", newImage);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                dto.ImagePath.CopyTo(fileStream);
            }

            return StatusCode(201, "Successfully added a product!");
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromForm] ProductDto dto,
            [FromServices] UpdateProductCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);

            if(dto.ImagePath != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(dto.ImagePath.FileName);
                var newImage = guid + extension;
                var path = Path.Combine("wwwroot", "images", newImage);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    dto.ImagePath.CopyTo(fileStream);
                }
            }

            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id, [FromServices] DeleteProductCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}


