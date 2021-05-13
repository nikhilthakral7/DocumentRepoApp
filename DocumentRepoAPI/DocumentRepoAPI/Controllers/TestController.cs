using DocumentRepoAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace DocumentRepoAPI.Controllers
{
    public class UserContoller : ApiController
    {
        private readonly IUserService userObj;
        public UserContoller(IUserService userObj)
        {
            this.userObj = userObj;
        }
        [HttpGet]
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("api/getsomething/{id}/getsomethingelse/{value}")]
        // GET api/<controller>/5
        public string Get(int id, int value)
        {
            //userObj.CreateUsers();
            return userObj.DeleteUsers(1).ToString();
        }

        [HttpPost]
        [Route("api/postsomething")]
        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        [HttpPost]
        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}