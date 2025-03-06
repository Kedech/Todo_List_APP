using Todo_List_API.DTOs;
using Todo_List_API.Entities;
using Todo_List_API.Helpers;
using Todo_List_API.Repositories.Interfaces;
using Todo_List_API.Services.Interfaces;

namespace Todo_List_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerUserDTO.Email!);

            if (existingUser.Id != 0) throw new Exception("User already exists");

            var (hashedPassword, salt) = MethodsHelpers.HashPassword(registerUserDTO.Password!);
            var user = new User
            {
                Name = registerUserDTO.Name,
                Email = registerUserDTO.Email,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            await _userRepository.CreateAsync(user);
            return MethodsHelpers.GenerateJwtToken(user, _configuration);
        }        

        public async Task<string> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userRepository.GetByEmailAsync(loginUserDTO.Email!);

            if (user.Id == 0) throw new Exception("User does not exist");

            if (MethodsHelpers.VerifyPassword(loginUserDTO.Password!, user.PasswordHash!, user.Salt!))
            {
                return MethodsHelpers.GenerateJwtToken(user, _configuration);
            }
            else
            {
                throw new Exception("Invalid password");
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
    }
}