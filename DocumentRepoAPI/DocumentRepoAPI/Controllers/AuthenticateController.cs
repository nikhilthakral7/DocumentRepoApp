using DocumentRepoAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DocumentRepoAPI.Services.Interfaces;
using DocumentRepoAPI.Data.Validators;

namespace DocumentRepoAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthenticateController : ApiController
    {
        private readonly IAuthenticationService authObj;
        //using dependency injection
        public AuthenticateController(IAuthenticationService authObj)
        {
            this.authObj = authObj;
        }

        //Attribute to bypass authorization
        [Route("")]
        [OverrideAuthorization]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginDTO user)
        {
            var validator = new LoginDTOValidator();
            var output = validator.Validate(user);
            if (!output.IsValid)
            {
                return BadRequest(String.Join(", ", output.Errors.Select(x => x.ErrorMessage)));
            }
            var res = await authObj.Login(user);
            if (res != null)
            {
                //shouldn' we return userDTO obj instead of token only,which contain unserId also
                return Ok(res);
            }
            return BadRequest();
        }
        [Route("{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> LogOut(long userId)
        {
            var res = await authObj.LogOut(userId);
            if(res!=-1)
                return Ok(new { message = "Logout successfull!"});
            return BadRequest();
        }
    }
}
