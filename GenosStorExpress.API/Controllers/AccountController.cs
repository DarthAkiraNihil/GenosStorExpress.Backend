using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GenosStorExpress.API.Wrappers;
using GenosStorExpress.Domain.Entity.User;
using GenosStorExpress.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GenosStorExpress.API.Controllers;

/// <summary>
/// Контроллер <c>AccountController</c> предоставляет стандартные методы для управления аккаунтами.
/// </summary>
[ApiController]
[Route("api/account")]
public class AccountController : AbstractController {
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="userManager">Стандартный менеджер пользователей</param>
    /// <param name="signInManager">Менеджер аутентификации</param>
    /// <param name="configuration">Конфигурация</param>
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) : base(userManager) {
        _signInManager = signInManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Регистрация в системе
    /// </summary>
    /// <param name="data">Данные для регистрации</param>
    /// <returns>Ничего в случае успеха, иначе структуру содержащую список ошибок</returns>
    [HttpPost("sign_up")]
    public async Task<IActionResult> SignUp(SignUpDataWrapper data)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        switch (data.UserType) {
            case "administrator": {
                return BadRequest(new DetailObject("Создавать новых администраторов таким способом запрещено!"));
            }
            case "individual_entity": {
                try {
                    
                    string name = data.AdditionalData["name"].GetString();
                    string surname = data.AdditionalData["surname"].GetString();
                    string phone = data.AdditionalData["phone_number"].GetString();

                    var created = new IndividualEntity {
                        UserName = data.Email,
                        Email = data.Email,
                        Name = name,
                        Surname = surname,
                        PhoneNumber = phone
                    };
                    
                    var result = await _userManager.CreateAsync(created, data.Password);
                    if (result.Succeeded) {
                        await _userManager.AddToRoleAsync(created, data.UserType);
                        return Ok(new { Message = $"Физическое лицо {data.Email} было успешно создано" });
                    }
                    
                    return BadRequest(result.Errors);
                    
                } catch (Exception e) {
                    return BadRequest(new DetailObject(e.Message));
                }
            }
            case "legal_entity": {
                try {
                    
                    long inn = data.AdditionalData["inn"].GetInt64();
                    long kpp = data.AdditionalData["kpp"].GetInt64();
                    string physicalAddress = data.AdditionalData["physical_address"].GetString();
                    string legalAddress = data.AdditionalData["legal_address"].GetString();

                    var created = new LegalEntity {
                        UserName = data.Email,
                        Email = data.Email,
                        INN = inn,
                        KPP = kpp,
                        LegalAddress = legalAddress,
                        PhysicalAddress = physicalAddress,
                        IsVerified = false
                    };
                    
                    var result = await _userManager.CreateAsync(created, data.Password);
                    if (result.Succeeded) {
                        await _userManager.AddToRoleAsync(created, data.UserType);
                        return Ok(new { Message = $"Юридическое лицо {data.Email} было успешно создано. Ожидайте верификации" });
                    }
                    
                    return BadRequest(result.Errors);
                    
                } catch (Exception e) {
                    return BadRequest(new DetailObject(e.Message));
                }
            }
            default: {
                return BadRequest(new DetailObject($"Неизвестный тип пользователя - {data.UserType}"));
            }
                
        }
        
    }

    /// <summary>
    /// Вход в систему
    /// </summary>
    /// <param name="model">Данные для входа</param>
    /// <returns>Токен и роль пользователя в случае успеха, иначе 401</returns>
    [HttpPost("sign_in")]
    public async Task<IActionResult> SignIn(SignInDataWrapper model)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded) {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) {
                return BadRequest(new DetailObject("Неверное имя пользователя"));
            }
            if (user is LegalEntity legalEntity && !legalEntity.IsVerified) {
                return BadRequest(new DetailObject("Юридическое лицо не подтверждено"));
            }
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token, Role = user.UserType });
        }
        return Unauthorized();
    }

    /// <summary>
    /// Выход из системы
    /// </summary>
    /// <returns>Ничего в случае успеха</returns>
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await _signInManager.SignOutAsync();
        return NoContent();
    }

    /// <summary>
    /// Метод проверки активности сессии
    /// </summary>
    /// <returns>201 в случае успеха, иначе 401</returns>
    [HttpGet("validate")]
    public async Task<IActionResult> ValidateToken() {
        User? user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null) {
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        IList<string> roles = await _userManager.GetRolesAsync(user);
        string? userRole = roles.FirstOrDefault();
        return Ok(new { message = "ACCESS GRANTED", username = user.UserName, userRole });

    }

    private string GenerateJwtToken(User user) {
        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var roles = _userManager.GetRolesAsync(user).Result;
        claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}