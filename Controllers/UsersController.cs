using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ams_finstek_dotnet.Data;
using ams_finstek_dotnet.Data.Entities;

namespace ams_finstek_dotnet.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IAmsRepository _repository;
        public UsersController(IAmsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get(int id, string name, string active)
        {
            
                System.Diagnostics.Debug.WriteLine("Trying");
                return Ok(_repository.GetAllUsers(id, name, active));
                
            
            
            
        }

        /*
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _repository.GetUserById(id);
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */

        public IActionResult Post ([FromBody]User model)
        {
            try
            {
                _repository.AddEntity(model);
                if (_repository.SaveAll())
                {
                    return Created($"/api/users/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Failed to save new user");
        }
    }
}
