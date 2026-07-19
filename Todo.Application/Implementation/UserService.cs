using AutoMapper;
using Todo.Application.Contracts;
using Todo.Application.DTOs.Request;
using Todo.Domain.DomainEntities;
using Todo.Domain.RepositoryInterface;

namespace Todo.Application.Implementation;

public class UserService(IUserRepository userRepository,IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<bool> CreateUserAsync(CreateUserDto userDto)
    {
        //dto to domain
        var userDomain = mapper.Map<UserDomain>(userDto);
        userDomain.PasswordHash= BCrypt.Net.BCrypt.HashPassword(userDto.Password);
       await userRepository.AddAsync(userDomain);
       var response = await userRepository.CommitAsync();
       return response > 0;
    }
}