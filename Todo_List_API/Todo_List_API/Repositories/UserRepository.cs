using Microsoft.EntityFrameworkCore;
using Todo_List_API.Contexts;
using Todo_List_API.Entities;
using Todo_List_API.Repositories.Interfaces;

namespace Todo_List_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContextUser _context;

        public UserRepository(DataContextUser dataContext)
        {
            _context = dataContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            return user ?? new User();
        }

        public async Task<User> UpdateAsync(User user)
        {   
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
