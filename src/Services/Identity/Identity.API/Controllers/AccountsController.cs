using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Infrastructure;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController
        (
            IMapper mapper,
            UserManager<ApplicationUser> userManager
        )
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel userModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = _mapper.Map<ApplicationUser>(userModel);

                var identityResult = await _userManager.CreateAsync(newUser, userModel.Password);

                if (identityResult.Succeeded)
                {
                    var addClaimsResult = await _userManager.AddClaimsAsync(newUser, new[] { new Claim(JwtClaimTypes.Email, userModel.Email)});

                    if (addClaimsResult.Succeeded)
                    {
                        return StatusCode((int)HttpStatusCode.Created, "The user has been registered successfully.");
                    }

                    return BadRequest(addClaimsResult.Errors.Descriptions());
                }

                return BadRequest(identityResult.Errors.Descriptions());
            }

            return BadRequest(ModelState.Errors());
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Ok("Thank you for confirming your email.");
            }

            return BadRequest();
        }

        /// <summary>
        ///     Check is username already exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyUsername(string username)
        {
            //var user = await _userManager.Users.SingleOrDefaultAsync(u =>
            //    u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            var user = await _userManager.FindByNameAsync(username);

            return user != null ? Json($"Username {username} is already in use.") : Json(true);
        }
    }

    public static class ModelStateServiceExtensions
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState)
        {
            var allErrors = modelState.Values.SelectMany(v => v.Errors);

            return allErrors.Select(e => e.ErrorMessage);
        }
    }

    public static class IdentityErrorServiceExtensions
    {
        public static IEnumerable<string> Descriptions(this IEnumerable<IdentityError> identityErrors)
        {
            return identityErrors.Select(e => e.Description);
        }
    }

    public class UserRegistrationModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        [RegularExpression(@"(^[\w]+$)", ErrorMessage = "Only letters and numbers")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, ApplicationUser>();
        }
    }
}