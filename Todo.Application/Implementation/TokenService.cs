using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Todo.Application.Contracts;
using Todo.Application.DTOs.Request;
using Todo.Application.DTOs.Response;
using Todo.Application.Exceptions;
using Todo.Domain.DomainEntities;
using Todo.Domain.RepositoryInterface;

namespace Todo.Application.Implementation;

public class TokenService(IUserRepository userRepository,IPasswordHasher passwordHasher,IConfiguration configuration )
    :ITokenService
{
    public async Task<TokenResponseDto> GetTokenAsync(TokenRequestDto requestDto)
    {
        var userDomain = await userRepository.GetByEmailAsync(requestDto.UserName);
        if (userDomain == null)
            throw new InvalidEmailException(ErrorConstants.InvalidEmail);

        if (!passwordHasher.VerifyPassword(requestDto.Password, userDomain.PasswordHash))
            throw new InvalidEmailException(ErrorConstants.InvalidPassword);

        string token =
            GenerateToken(userDomain);

        return new TokenResponseDto(token, string.Empty);
    }    private string GenerateToken(UserDomain userResponse)
    {
        string secretKey = configuration["Jwt:Secret"]!;

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor
            = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, userResponse.FullName),
                    new Claim(ClaimTypes.Email, userResponse.Email),
                    new Claim("UserId", userResponse.Id.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:TokenExpiryInMinutes"])),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

        var tokenHandler = new JsonWebTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return token;
    }
}

public class ErrorConstants
{
    public static string InvalidEmail { get; set; }
    public static string InvalidPassword { get; set; }
}