using Microsoft.AspNetCore.Identity;
using System.Text;
using TugasBootcampNET.DTO;

namespace TugasBootcampNET.DAL
{
    public class UserEF : IUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserEF(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<UserGetDTO> Authenticate(AddUserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserGetDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Registration(AddUserDTO addUserDTO)
        {
            try
            {
                var user = new IdentityUser
                {
                    UserName = addUserDTO.Username,
                    Email = addUserDTO.Username
                };
                var result = await _userManager.CreateAsync(user, addUserDTO.Password);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in result.Errors)
                    {
                        sb.Append($"{item.Code}:{item.Description}\n");
                    };
                    throw new Exception(sb.ToString());
                }   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
