using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Models;
using MusicAPI.Repos;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo) { 
            _userRepo = userRepo;
        }
        [HttpPost]
        public IActionResult PostUser(UserModel user)
        {
            int? op = _userRepo.AddUser(user);
            if (op != null) { 
                return Ok("User Added");
            }
            return BadRequest("User Alredy Exist or playlist does not exist");
        }
        [HttpGet]
        public IActionResult GetUser() {
            return( Ok(_userRepo.GetUserList()));
        }
        [HttpGet("{name}")]
        public IActionResult GetById(string name) {
            return Ok(_userRepo.GetUserById(name));
        }

        [HttpDelete("{User}")]
        public IActionResult DeleteUser(string User)
        {
            int? op = _userRepo.DeleteUser(User);
            if (op != null)
            {
                return Ok("Deleted");
            }
            return BadRequest("Not Found");
        }
        [HttpPut("{User}")]
        public IActionResult PutUser(string User,UserModel usermodel)
        {
            int? op = _userRepo.EditUser(User, usermodel);
            if (op != null)
            {
                return Ok("Updated");
            }
            return BadRequest("Not Found(User or Playlist)");
        }

    }
}
