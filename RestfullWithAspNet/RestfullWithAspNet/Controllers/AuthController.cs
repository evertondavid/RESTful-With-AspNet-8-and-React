using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestfullWithAspNet.Business;
using RestfullWithAspNet.Data.VO;

namespace RestfullWithAspNet.Controllers
{
    /// <summary>
    /// Controller class for handling authentication-related requests.
    /// </summary>
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="loginBusiness">The login business implementation.</param>
        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        /// <summary>
        /// Handles the signin request.
        /// </summary>
        /// <param name="user">The user object containing the credentials.</param>
        /// <returns>The result of the signin operation.</returns>
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        /// <summary>
        /// Handles the refresh request.
        /// </summary>
        /// <param name="tokenVo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null) return BadRequest("Invalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("Invalid client request");
            return Ok(token);
        }

        /// <summary>
        /// Handles the revoke request.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("User name is null or empty.");
            }
            var result = _loginBusiness.RevokeToken(userName);
            if (!result) return BadRequest("Invalid client request");
            return NoContent();
        }
    }
}
