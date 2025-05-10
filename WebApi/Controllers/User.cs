using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Domain.DTO;


namespace WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class MerchantController : ControllerBase
{
    private readonly RegisterUserService _registerservice;
    private readonly LoginMerchantService _loginService;
    public MerchantController(RegisterUserService registerservice, LoginMerchantService loginService) 
    { 
        _registerservice = registerservice;
        _loginService = loginService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserAuthDto dto)
    {
        var merchant = await _registerservice.RegisterAsync(dto);
        return Ok(merchant);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserAuthDto request)
    {
        var merchant = await _loginService.LoginAsync(request.Username, request.Password);
        if (merchant == null) return Unauthorized();
        return Ok(merchant);
    }
}