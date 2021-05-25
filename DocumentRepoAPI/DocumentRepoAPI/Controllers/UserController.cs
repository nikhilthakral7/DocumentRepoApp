using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Data.Validators;
using DocumentRepoAPI.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System;
using DocumentRepoAPI.Filters;

namespace DocumentRepoAPI.Controllers
{
    //[Route("api/[Controller]")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService userObj;
        //getting obj of IUserSerivce from UnityContainer
        public UserController(IUserService userObj)
        {
            this.userObj = userObj;
        }

        [Route("")]
        [HttpGet]
        [RolesFilter("RegularUser,Admin")]
        public IHttpActionResult Get()
        {
            var res= userObj.GetUsers();
            if (res != null)
                return Ok(res);
            return NotFound();
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(long id)
        {
            var res = userObj.GetUsers(id: id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Users user)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Invalid Data Entered!");
            var Validator = new UserValidator();
            var output = Validator.Validate(user);
            if (!output.IsValid)
                return BadRequest(String.Join(", ", output.Errors.Select(x=>x.ErrorMessage)));
            var res =  await userObj.CreateUsers(user);
            return Ok(res);
        }
        [Route("api/user/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(long id, Users user)
        {
            var Validator = new UserValidator();
            var output = Validator.Validate(user);
            if (!output.IsValid)
                return BadRequest(String.Join(", ", output.Errors.Select(x => x.ErrorMessage)));
            var res = await userObj.UpdateUsers(id, user);
            if(res!=-1)
                return Ok();
            return NotFound();
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var res = await userObj.DeleteUsers(id);
            if (res != -1)
                return Ok(res);
            return NotFound();
        }

        [Route("/admin/{id}")]
        [RolesFilter("Admin")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteByAdmin(long id)
        {
            var res = await userObj.DeleteUsers(id);
            if (res != -1)
                return Ok(res);
            return NotFound();
        }
    }
}
