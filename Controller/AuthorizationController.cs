using BlogSite.Service;
using BlogSite.ViewModel.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
         private readonly AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginIn([FromBody] LoginRequestViewModel input)
        {
            return Ok(await _authorizationService.LoginIn(input));
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] LoginRequestViewModel input)
        {
            await _authorizationService.ResetPassword(input);
            return Ok();
        }
        
    }
}