using APIBloc.Models;
using APIBloc.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIBloc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            if (!SiteService.GetAll(out var results))
                return StatusCode(500);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!SiteService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(Site value)
        {
            if (!SiteService.Post(value.City, out int insertedId))
                return StatusCode(500);

            if (!SiteService.Get(insertedId, out var result))
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Site value)
        {
            if (!SiteService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound("Element not found.");

            if (!SiteService.Update(id, value.City))
                return StatusCode(500);

            if (!SiteService.Get(id, out result))
                return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!SiteService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound("Element not found.");

            if (!SiteService.GetDeletionPossibility(id, out bool canDelete))
                return StatusCode(500);

            if (!canDelete)
                return BadRequest("Cannot delete this row.");

            if (!SiteService.Delete(id))
                return StatusCode(500);

            return Ok();
        }
    }
}
