using Todo_List_API.Entities;

namespace Todo_List_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> GetByEmailAsync(string email);
    }
}
