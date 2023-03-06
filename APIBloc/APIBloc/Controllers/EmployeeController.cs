using APIBloc.Models;
using APIBloc.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIBloc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            if (!EmployeeService.GetAll(out var results))
                return StatusCode(500);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!EmployeeService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("Filtered")]
        public IActionResult GetFiltered(string? firstname, string? lastname, int? service, int? site)
        {
            if (!EmployeeService.GetFiltered(firstname, lastname, service, site, out var result))
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(Employee value)
        {
            if (!EmployeeService.Post(value, out int insertedId))
                return StatusCode(500);

            if (!EmployeeService.Get(insertedId, out var result))
                return StatusCode(500);

            return Ok(result);
        }
        /*
        [HttpPost("Multiple")]
        public IActionResult PostMultiple(List<JsonTruc> value)
        {
            foreach (JsonTruc item in value)
            {
                Employee employee = new Employee()
                {
                    Firstname = item.name.first,
                    Lastname = item.name.last,
                    MobilePhone = item.cell,
                    HomePhone = item.phone,
                    Email = item.email,
                    IDService = Random.Shared.Next(1, 6),
                    IDSite = Random.Shared.Next(1, 6)
                };

                if (!EmployeeService.Post(employee, out int insertedId))
                    return StatusCode(500);
            }

            return Ok();
        }

        public class JsonTruc
        {
            public JsonName name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string cell { get; set; }
        }

        public class JsonName
        {
            public string title { get; set; }
            public string first { get; set; }
            public string last { get; set; }

        }
        */

        [HttpPut("{id}")]
        public IActionResult Put(int id, Employee value)
        {
            if (!EmployeeService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound("Element not found.");

            if (!EmployeeService.Update(id, value))
                return StatusCode(500);

            if (!EmployeeService.Get(id, out result))
                return StatusCode(500);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!EmployeeService.Get(id, out var result))
                return StatusCode(500);

            if (result == null)
                return NotFound("Element not found.");

            if (!EmployeeService.Delete(id))
                return StatusCode(500);

            return Ok();
        }
    }
}
