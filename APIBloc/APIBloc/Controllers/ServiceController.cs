using APIBloc.Models;
using APIBloc.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIBloc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            if (!ServiceService.GetAll(out var results))
                return StatusCode(500);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!ServiceService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(Service value)
        {
            if (!ServiceService.Post(value.Name, out int insertedId))
                return StatusCode(500);

            if (!ServiceService.Get(insertedId, out var result))
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Service value)
        {
            if (!ServiceService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound("Element not found.");

            if (!ServiceService.Update(id, value.Name))
                return StatusCode(500);

            if (!ServiceService.Get(id, out result))
                return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ServiceService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound("Element not found.");

            if (!ServiceService.GetDeletionPossibility(id, out bool canDelete))
                return StatusCode(500);

            if (!canDelete)
                return BadRequest("Cannot remove this row.");

            if (!ServiceService.Delete(id))
                return StatusCode(500);

            return Ok();
        }
    }
}
