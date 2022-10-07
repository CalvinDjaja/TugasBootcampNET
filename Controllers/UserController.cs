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
    }
}
