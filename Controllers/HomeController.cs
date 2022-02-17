using ams_finstek_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ams_finstek_dotnet.Data;
using ams_finstek_dotnet.Data.Entities;


namespace ams_finstek_dotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly IAmsRepository _repository;

        public HomeController(ILogger<HomeController> logger, IAmsRepository repository)
        {
            _logger = logger;
            _repository = repository;
            
        }

        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("services")]
        public IActionResult Services()
        {
            var results = _repository.GetAllServices();
            return View(results);
        }

        [HttpGet("users/add")]
        public IActionResult UserAdd()
        {
            return View();
        }

        [HttpGet("users")]
        public IActionResult Users(int id, string name, string active, string email, string location)
        {

            var results = _repository.GetAllUsers(id, name, active, email, location);
            return View(results);
        }

        [HttpGet("json/users")]
        public JsonResult GetUsersJson(int id, string name, string active, string email, string location)
        {
            
            var results = _repository.GetAllUsers(id, name, active, email, location);
            return Json(results);
        }

        [HttpGet("json/accesses")]
        public JsonResult GetUserAccessesJson(int id)
        {
            var result = _repository.GetUserAccesses(id);
            return Json(result);
        }


        [HttpPost("json/users/add")]
        public IActionResult Post([FromBody] User model)
        {
            try
            {
                _repository.AddEntity(model);
                if (_repository.SaveAll())
                {
                    return Created($"/users/{model.Id}", model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Failed to save new user");
        }

        [HttpPost ("json/accesses/add")]
        public IActionResult Post([FromBody] Access model)
        {
            try
            {
                _repository.AddEntity(model);
                if (_repository.SaveAll())
                {
                    return Created($"Created: ", model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Failed to save access");
        }

        [HttpGet ("json/users/del")]
        public IActionResult DelUser(int id)
        {
            try
            {
                _repository.DeleteUser(id);
                if (_repository.SaveAll())
                {
                    return Ok("User with id: '" + id + "' has been deleted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest("Failed to save new user");
        }

        [HttpPut ("json/users/edit")]
        public IActionResult PUT(int id,[FromBody] User user)
        {
            _repository.EditUser(id,user);
            if (_repository.SaveAll())
            {
                return Ok("User with id :'" + id + "', was edited. New user data: 'name = '" + user.Name + "', email = '" + user.Email + "', location = '" + user.Location + "', active = '" + user.isActive + "'");
            }
            else
            {
                return BadRequest("Failed to edit user with id '" + id + "'");
            }
            
        }



        [HttpGet("users/{id:int}")]
        public IActionResult UserByID(int id)
        {
            try
            {
                var results = _repository.GetUserById(id);
                if (results != null)
                {
                    return View(results);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}