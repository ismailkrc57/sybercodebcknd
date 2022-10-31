using Microsoft.AspNetCore.Mvc;
using DataAccess.Concrete.EntityFramework;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private EfUserDal efUserDal;

        public UserController()
        {
            efUserDal = new EfUserDal();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = efUserDal.GetAll();
            return Ok(result);
        }

        [HttpGet("login")]
        public IActionResult Login(string username, string password)
        
        {
            var result = efUserDal.getByUsernameAndPassword(username, password);
            return Ok(result);
                
        }
        //
        // [HttpPost("add")]
        // public IActionResult Add(User user)
        // {
        //     var result = _userService.Add(user);
        //     if (result.Success)
        //     {
        //         return Ok(result.Message);
        //     }
        //
        //     return BadRequest(result.Message);
        // }
        //
        // [HttpPost("delete")]
        // public IActionResult Delete(User user)
        // {
        //     var result = _userService.Delete(user);
        //     if (result.Success)
        //     {
        //         return Ok(result.Message);
        //     }
        //
        //     return BadRequest(result.Message);
        // }
        //
        // [HttpPost("update")]
        // public IActionResult Update(User user)
        // {
        //     var result = _userService.Update(user);
        //     if (result.Success)
        //     {
        //         return Ok(result.Message);
        //     }
        //
        //     return BadRequest(result.Message);
        // }
        //
        // [HttpGet("getusers")]
        // public IActionResult GetUsers()
        // {
        //     var efUserDal = new EfUserDal();
        //     var result = efUserDal.getAllUsers();
        //   
        //         return Ok(result);
        //     
        //     
        // }
    }
}