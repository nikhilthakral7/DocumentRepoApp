using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace DocumentRepoAPI.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : ApiController
    {
        private readonly IUserService userObj;
        //getting obj of IUserSerivce from UnityContainer
        public UserController(IUserService userObj)
        {
            this.userObj = userObj;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var res= userObj.GetUsers();
            if (res == null)
                return Ok(res);
            return NotFound();
        }

        [Route("{id}")]
        [HttpGet]
        //public HttpResponseMessage Get(long id)
        //{
        //    var res = userObj.GetUsers(id: id);
        //    //var output = new JsonResult(userObj.GetUsers(id: id));

        //    return Request.CreateResponse(res==null?HttpStatusCode.NotFound:HttpStatusCode.OK, res) ;
        //}
        public IHttpActionResult Get(long id)
        {
            var res = userObj.GetUsers(id: id);
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public async Task<long> Post(Users user)
        {
            return await userObj.CreateUsers(user);
        }

        [HttpPut]
        public async Task<long> Put(long id, Users user)
        {
            return await userObj.UpdateUsers(id, user);
        }

        [HttpDelete]
        public long Delete(long id)
        {
            return userObj.DeleteUsers(id);
        }
    }
}
