using GEWAR.Models;
using Jiwar.Account.Services;
using Jiwar.Helpers;
using Jiwar.Services.GoogleService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Jiwar.Controllers.AuthController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AccountService accountService;

        public AuthController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("google-signin")]
        public async Task<IActionResult> GoogleSignIn([FromBody] string idToken)
        {
            var result = await accountService.GoogleSignInAsync(idToken);

            if (!result.Success)
                return BadRequest(result);  

            return Ok(result);
        }
        //ازم Angular يجيب idToken من Google Sign-In.

        //Backend مش هيقدر يختبر Google SignIn لوحده بدون idToken.

        //في Angular، هتستخدم مكتبة مثل @abacritt/angularx-social-login أو gapi.auth2 للحصول على idToken.

    }
}
