using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator){
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> RegisterAsync(string userName,string password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);
        if(user != null)
        {
            return false;
        }
        var hashPassword = _passwordHasher.HashPassword(password);
        user = new User(userName, hashPassword);
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return true;
    }

    public async Task<string?> LoginAsync(string userName, string password)
    {
        var user = await _userRepository.GetByUserNameAsync(userName);
        if(user is null)
        {
            return null;
        }
        var isValidPassword = _passwordHasher.VerifyPassword(password,user.PasswordHash);

        if(!isValidPassword)
        {
            return null;
        }
        return _jwtTokenGenerator.GenerateToken(user);
    }
}