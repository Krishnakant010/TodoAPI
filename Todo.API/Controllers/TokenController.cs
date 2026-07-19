using Microsoft.AspNetCore.Mvc;
using Todo.Application.Contracts;
using Todo.Application.DTOs.Request;
using Todo.Application.Implementation;

namespace Todo.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TokenController(ITokenService tokenService) :ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Get(TokenRequestDto tokenRequestDto)
    {
        return Ok(await tokenService.GetTokenAsync(tokenRequestDto));
    }
}