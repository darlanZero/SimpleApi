using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) 
        {
            try {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                if(loginDto == null) return BadRequest("Invalid data.");

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());
                if (user == null) return Unauthorized("Invalid email or password.");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (result.IsLockedOut) return Unauthorized("Account is locked out.");
                if (!result.Succeeded) return Unauthorized("Invalid username or password.");

                return Ok(new 
                {
                    user = new NewUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    },
                    message = "Login successful."
                });
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            try {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                if(registerDto == null) return BadRequest("Invalid data.");

                var appUser = new AppUser()
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                       return Ok(
                            new {
                                user = new NewUserDto
                                {
                                    UserName = appUser.UserName,
                                    Email = appUser.Email,
                                    Token = _tokenService.CreateToken(appUser)
                                },
                                message = "User created successfully."
                            }   
                       );
                    }
                    else
                    {
                        return BadRequest(new { message = "User role assignment failed.", errors = roleResult.Errors });
                    }
                }
                else
                {
                    return BadRequest(new { message = "User creation failed.", errors = createdUser.Errors });
                }
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}