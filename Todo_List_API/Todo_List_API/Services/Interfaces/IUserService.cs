using Todo_List_API.DTOs;
using Todo_List_API.Entities;

namespace Todo_List_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginUserAsync(LoginUserDTO loginUserDTO);
        Task<string> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<User> GetUserByEmailAsync(string email);
    }
}
