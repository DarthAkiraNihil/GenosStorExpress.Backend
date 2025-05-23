﻿using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
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
    /// <param name="logger">Логгер</param>
    /// <param name="signInManager">Менеджер аутентификации</param>
    /// <param name="configuration">Конфигурация</param>
    public AccountController(UserManager<User> userManager, ILogger<AccountController> logger, SignInManager<User> signInManager, IConfiguration configuration) : base(userManager, logger) {
        _signInManager = signInManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Регистрация в системе
    /// </summary>
    /// <param name="data">Данные для регистрации</param>
    /// <returns>Ничего в случае успеха, иначе структуру содержащую список ошибок</returns>
    [HttpPost("sign_up")]
    public async Task<IActionResult> SignUp(SignUpDataWrapper data) {

        IList<string> parameters = new List<string> {
            $"data = {data}",
        };
        
        if (!ModelState.IsValid) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Something is wrong with model state", _getCurrentUser(), true);
            return BadRequest(ModelState);
        }

        switch (data.UserType) {
            case "administrator": {
                var msg = "Создавать новых администраторов таким способом запрещено!";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }
            case "individual_entity": {
                try {
                    
                    string name = data.AdditionalData.GetProperty("name").GetString()!;
                    string surname = data.AdditionalData.GetProperty("surname").GetString()!;
                    string phone = data.AdditionalData.GetProperty("phone_number").GetString()!;

                    var created = new IndividualEntity {
                        Id = Guid.NewGuid().ToString(),
                        UserName = data.Email,
                        Email = data.Email,
                        Name = name,
                        Surname = surname,
                        PhoneNumber = phone
                    };
                    
                    created.CartId = created.Id;
                    created.Cart.CustomerId = created.Id;
                    
                    var result = await _userManager.CreateAsync(created, data.Password);
                    if (result.Succeeded) {
                        await _userManager.AddToRoleAsync(created, data.UserType);
                        var msg = $"Физическое лицо {data.Email} было успешно создано";
                        _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser());
                        return Ok(new { Message = msg });
                    }
                    
                    _log(MethodBase.GetCurrentMethod()!.Name, parameters, result.Errors.ToString()!, _getCurrentUser(), true);
                    return BadRequest(result.Errors);
                    
                } catch (Exception e) {
                    _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                    return BadRequest(new DetailObject(e.Message));
                }
            }
            case "legal_entity": {
                try {

                    long inn = data.AdditionalData.GetProperty("inn").GetInt64();
                    long kpp = data.AdditionalData.GetProperty("kpp").GetInt64();
                    string physicalAddress = data.AdditionalData.GetProperty("physical_address").GetString()!;
                    string legalAddress = data.AdditionalData.GetProperty("legal_address").GetString()!;

                    var created = new LegalEntity {
                        Id = Guid.NewGuid().ToString(),
                        UserName = data.Email,
                        Email = data.Email,
                        INN = inn,
                        KPP = kpp,
                        LegalAddress = legalAddress,
                        PhysicalAddress = physicalAddress,
                        IsVerified = false
                    };
                    
                    created.CartId = created.Id;
                    created.Cart.CustomerId = created.Id;
                    
                    var result = await _userManager.CreateAsync(created, data.Password);
                    if (result.Succeeded) {
                        await _userManager.AddToRoleAsync(created, data.UserType);
                        var msg = $"Юридическое лицо {data.Email} было успешно создано. Ожидайте верификации";
                        _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser());
                        return Ok(new { Message = msg });
                    }
                    
                    return BadRequest(result.Errors);
                    
                } catch (Exception e) {
                    _log(MethodBase.GetCurrentMethod()!.Name, parameters, e.Message, _getCurrentUser(), true);
                    return BadRequest(new DetailObject(e.Message));
                }
            }
            default: {
                var msg = $"Неизвестный тип пользователя - {data.UserType}";
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, msg, _getCurrentUser(), true);
                return BadRequest(new DetailObject(msg));
            }
                
        }
        
    }

    /// <summary>
    /// Вход в систему
    /// </summary>
    /// <param name="model">Данные для входа</param>
    /// <returns>Токен и роль пользователя в случае успеха, иначе 401</returns>
    [HttpPost("sign_in")]
    public async Task<IActionResult> SignIn(SignInDataWrapper model) {

        IList<string> parameters = new List<string> {
            $"model = {model}",
        };
        
        if (!ModelState.IsValid) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Something is wrong with model state", _getCurrentUser(), true);
            return BadRequest(ModelState);
        }
        
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
        if (result.Succeeded) {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Неверное имя пользователя", _getCurrentUser(), true);
                return BadRequest(new DetailObject("Неверное имя пользователя"));
            }
            if (user is LegalEntity legalEntity && !legalEntity.IsVerified) {
                _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Юридическое лицо не подтверждено", _getCurrentUser(), true);
                return BadRequest(new DetailObject("Юридическое лицо не подтверждено"));
            }

            var token = GenerateJwtToken(user);
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access granted", _getCurrentUser());
            return Ok(new { Token = token, Role = user.UserType.ToString(), Username = user.Email });
        }
        return Unauthorized();
    }

    /// <summary>
    /// Выход из системы
    /// </summary>
    /// <returns>Ничего в случае успеха</returns>
    [Authorize]
    [HttpPost("sign_out")]
    public new async Task<IActionResult> SignOut() {
        await _signInManager.SignOutAsync();
        return NoContent();
    }

    /// <summary>
    /// Метод проверки активности сессии
    /// </summary>
    /// <returns>201 в случае успеха, иначе 401</returns>
    [HttpGet("validate")]
    public async Task<IActionResult> ValidateToken() {

        IList<string> parameters = new List<string> {
            "<no parameters>",
        };
        
        
        User? user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null) {
            _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Unauthorized", _getCurrentUser(), true);
            return Unauthorized(new { message = "Доступ запрещён" });
        }
        IList<string> roles = await _userManager.GetRolesAsync(user);
        string? userRole = roles.FirstOrDefault();
        _log(MethodBase.GetCurrentMethod()!.Name, parameters, "Access granted", _getCurrentUser());
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