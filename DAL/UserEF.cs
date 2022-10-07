using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TugasBootcampNET.DTO;
using TugasBootcampNET.Helpers;

namespace TugasBootcampNET.DAL
{
    public class UserEF : IUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        public UserEF(UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<UserGetDTO> Authenticate(AddUserDTO user)
        {
            var currUser = await _userManager.FindByNameAsync(user.Username);
            var userResult = await _userManager.CheckPasswordAsync(currUser, user.Password);
            if (!userResult)
                throw new Exception($"Authentication failed");

            UserGetDTO userWithToken = new UserGetDTO
            {
                Username = user.Username
            };

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token = tokenHandler.WriteToken(token);
            return userWithToken;
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
