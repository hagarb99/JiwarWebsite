//using GEWAR.Models;
//using Jiwar.Account.Services;
//using Jiwar.DTOs;
//using Jiwar.Helpers;
//using Jiwar.Services.GoogleService;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;

//namespace Jiwar.Controllers.AuthController
//{  
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly AccountService accountService;

//        public AuthController(AccountService accountService)
//        {
//            this.accountService = accountService;
//        }
//        [HttpPost("google-signin")]
//        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInRequest req)
//        {
//            if (string.IsNullOrEmpty(req?.IdToken))
//                return BadRequest("IdToken is required");

//            var result = await accountService.GoogleSignInAsync(req.IdToken);

//            return result.Success
//                ? Ok(result)  
//                : BadRequest(result);
            //var result = await accountService.GoogleSignInAsync(req?.IdToken);

            //if (!result.Success)
            //    return BadRequest(result);

            //return Ok(result);
        //}
        //public async Task<IActionResult> GoogleSignIn([FromBody] string idToken)
        //{
        //    var result = await accountService.GoogleSignInAsync(idToken);

        //    if (!result.Success)
        //        return BadRequest(result);  

        //    return Ok(result);
        //}
        //ازم Angular يجيب idToken من Google Sign-In.

        //Backend مش هيقدر يختبر Google SignIn لوحده بدون idToken.

        //في Angular، هتستخدم مكتبة مثل @abacritt/angularx-social-login أو gapi.auth2 للحصول على idToken.

//    }
//}
