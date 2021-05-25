using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DocumentRepoAPI.Controllers
{
    [RoutePrefix("api/docs")]
    public class DocumentController : ApiController
    {
        private readonly IDocumentService docObj;
        public DocumentController(IDocumentService docObj)
        {
            this.docObj = docObj;
        }

        [Route("{userId}")]
        [HttpPost]
        public async Task<IHttpActionResult> PostFile(long userId)
        {
            //req
            var req = HttpContext.Current.Request;
            foreach (string file in req.Files)
            {
                //file.SaveAs(path);
                docObj.AddFile(userId, req.Files[file]);
                
            }
            return Ok();
        }
    }
}
