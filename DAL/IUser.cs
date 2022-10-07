using TugasBootcampNET.DTO;

namespace TugasBootcampNET.DAL
{
    public interface IUser
    {
        Task Registration(AddUserDTO addUserDTO);
        Task<IEnumerable<UserGetDTO>> GetAll();
        Task<UserGetDTO> Authenticate(AddUserDTO user);
    }
}
