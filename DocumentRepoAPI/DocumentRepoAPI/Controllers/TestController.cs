using System.Collections.Generic;
using System.Web.Http;

namespace DocumentRepoAPI.Controllers
{
    public class TestController : ApiController
    {
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
            return "value";
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