using TugasBootcampNET.DTO;

namespace TugasBootcampNET.DAL
{
    public interface IUser
    {
        Task Registration(AddUserDTO addUserDTO);
        IEnumerable<UserGetDTO> GetAll();
        Task<UserGetDTO> Authenticate(AddUserDTO user);
    }
}
