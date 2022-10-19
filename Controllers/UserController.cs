using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TugasBootcampNET.DAL;
using TugasBootcampNET.DTO;

namespace TugasBootcampNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AddUserDTO addUserDTO)
        {
            try
            {
                await _user.Registration(addUserDTO);
                return Ok($"Registrasi User {addUserDTO.Username} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(AddUserDTO addUserDTO)
        {
            try
            {
                var user = await _user.Authenticate(addUserDTO);
                if (user == null)
                    return BadRequest("Username or Password doesn't match");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
