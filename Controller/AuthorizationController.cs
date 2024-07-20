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
        public async Task<IActionResult> CreateAuthorization([FromBody] LoginRequestViewModel input)
        {
            return Ok(await _authorizationService.GenerateJwtToken(input));
        }

        [HttpPost("ok")]
        public async Task<IActionResult> CreateAuthorization11([FromBody] xyz input)
        {
            await _authorizationService.SendEmail(input.toEmail,input.subject,input.body);
            return Ok();
        }
    }
}