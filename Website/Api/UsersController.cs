
using Business.IRepostitory;
using Common;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Website.Models;
using Website.Services;

namespace Website.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
 
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UsersController> _logger;
        private readonly ITokenService _tokenService;
       
        public UsersController(SignInManager<ApplicationUser> signInManager,
           ILogger<UsersController> logger,
           UserManager<ApplicationUser> userManager, ITokenService tokenService
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
           
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        // To do
                        var token = _tokenService.GenerateAccessToken(user, 0, _userManager);
                     
                        return Ok(token);
                    }
                    else
                    {
                        return BadRequest(new { message = MessageUserConstants.PasswordIncorrect });
                    }

                }
                else
                {
                    return BadRequest(new { message = MessageUserConstants.UserNameIncorrect });
                }

            }
            return BadRequest(new { message = MessageUserConstants.BadRequest });


        }
    }
}
