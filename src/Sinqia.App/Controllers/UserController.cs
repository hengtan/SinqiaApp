using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sinqia.App.Enum;
using Sinqia.App.Models;
using Sinqia.App.Observable;
using Sinqia.App.Services.Interfaces;

namespace Sinqia.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserValidator userValidator) : ControllerBase
    {
        private readonly IUserValidator _userValidator = userValidator;
        private const string ValidationErrorMsg = "Please username can't be empty and the password shouldn't be less 6 character";

        [HttpGet]
        public async Task<IActionResult> ValidateUser(User user)
        {
            if (await _userValidator.ValidateUser(user))
            {
                return Ok("User validated!");
            }

            return BadRequest(ValidationErrorMsg);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateNewUser(UserType userType, User user)
        {
            var response = await _userValidator.CreateNewUser(userType, user);
            return response != null ? Ok(response) : BadRequest("Error creating the user");
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateObservableUser()
        {
            var response = await _userValidator.TestObservable();
            return Ok(response);
        }

    }
}