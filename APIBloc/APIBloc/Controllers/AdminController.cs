using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace APIBloc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("TryLogin/{password}")]
        public IActionResult TryLogin(string? password)
        {
            string sql = "SELECT `Password` FROM `Admin` LIMIT 1";

            try
            {
                using MySqlDataReader reader = sql.ExecuteReaderToDB();

                if (reader.Read())
                {
                    string? realPassword = reader.GetString("password");

                    if (realPassword == password)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return Ok(true);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
