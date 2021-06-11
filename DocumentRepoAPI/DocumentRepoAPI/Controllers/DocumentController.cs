using DocumentRepoAPI.Data.Entities;
using DocumentRepoAPI.Data.Validators;
using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("file/down/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> DownloadFile(long fileId)
        {
            var f = await docObj.DownloadFilesByUser(fileId);
            //Request.Content = new fi(f);
            return Ok();

        }

        [Route("file/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFile(long userId)
        {
            var output = await docObj.GetFilesByUser(userId);
            return Ok(output);
        }

        [Route("file/{userId}/{folderName}")]
        [HttpPost]
        public async Task<IHttpActionResult> PostFile(long userId, string folderName)
        
        {

            //req
            //var req = HttpContext.Current.Request;
            var userIdFromHeader = Request.Headers.GetValues("userId").FirstOrDefault();
            //Checking wether the userId in the url matces with the user id of the user to whoom token belongs
            if (Convert.ToInt64(userIdFromHeader) != userId)
                return Unauthorized();

            foreach (string file in HttpContext.Current.Request.Files)
            {
                //file.SaveAs(path);
                var res = await docObj.AddFile(userId, folderName, HttpContext.Current.Request.Files[file]);
                if (!res)
                {
                    return BadRequest();
                }
            }
            return Ok();
        }



        [Route("file/{userId}/{fileId}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateFile(long userId, long fileId)
        {
            var req = HttpContext.Current.Request;
            var res = new Dictionary<long, long>();
            foreach (string file in req.Files)
            {
                var r = await docObj.UpdateFile(userId, fileId, req.Files[file]);
                res.Add(fileId, r);

            }
            return Ok(res);
        }

        [Route("file/{userID}/{fileId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFile(long userId, long fileId)
        {
            var res = await docObj.DeleteFile(userId, fileId);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }


        [Route("file/perm/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFilePermission(long userId)
        {
            var output = await docObj.GetFilePermission(userId);
            return Ok(output);
        }

        [Route("file/perm")]
        [HttpPost]
        public async Task<IHttpActionResult> GrantFilePermission(SharedFiles shFile)
        {
            var validator = new SharedFileValidator();
            var output = validator.Validate(shFile);
            if (output.IsValid)
            {
                var res = await docObj.GrantFilePermission(shFile);
                if (res)
                {
                    return Ok();
                }
                return BadRequest();

            }
            return BadRequest();
        }


        [Route("file/perm/mod")]
        [HttpPost]
        public async Task<IHttpActionResult> ModifyFilePermission(SharedFiles shFile)
        {
            var validator = new SharedFileValidator();
            var output = validator.Validate(shFile);
            if (output.IsValid)
            {
                var res = await docObj.ModifyFileAccess(shFile);
                if (!res)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }


        //[Route("file/{userId}")]
        //[HttpPut]
        //public async Task<IHttpActionResult> UpdateFile(long userId, string newName)
        //{
        //        var res = await docObj.UpdateFile(userId, newName);
        //        if (res != -1)
        //        {
        //            return Ok();
        //        }
        //        return BadRequest();
        //}


        //===============================================

        [Route("dir/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFolder(long userId)
        {
            var output = await docObj.GetFolderByUser(userId);
            return Ok(output);
        }

        [Route("dir")]
        [HttpPost]
        public async Task<IHttpActionResult> PostDirectory(Folders newFolder)
        {
            var validator = new FoldersValidator();
            var output = validator.Validate(newFolder);
            if (output.IsValid)
            {

                var res = await docObj.AddDirectory(newFolder);
                if (res != -1)
                {
                    return Ok(res);
                }
                return BadRequest("Folder Already exists");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("dir/{userId}/{folderId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteDirectory(long userId, long folderId)
        {
            var res = await docObj.DeleteDirectory(userId, folderId);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("dir/perm/{userId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFolderPermission(long userId)
        {
            var output = await docObj.GetFolderPermission(userId);
            return Ok(output);
        }

        [Route("dir/perm")]
        [HttpPost]
        public async Task<IHttpActionResult> GrantDirectoryPermission(SharedFolders shfolder)
        {
            var validator = new SharedFolderValidator();
            var output = validator.Validate(shfolder);
            if (output.IsValid)
            {
                var res = await docObj.GrantFolderPermission(shfolder);
                if (res)
                {
                    return Ok();
                }
                return BadRequest();

            }
            return BadRequest();
        }

        [Route("dir/perm/mod")]
        [HttpPost]
        public async Task<IHttpActionResult> ModifyDirectoryPermission(SharedFolders shfolder)
        {
            var validator = new SharedFolderValidator();
            var output = validator.Validate(shfolder);
            if (output.IsValid)
            {
                var res = await docObj.ModifyDirectoryAccess(shfolder);
                if (!res)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }


        [Route("dir/{userId}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateDirectory(long userId, Folders updatedFolder)
        {
            var validator = new FoldersValidator();
            var output = validator.Validate(updatedFolder);
            if (output.IsValid)
            {
                var res = await docObj.UpdateDirectory(userId, updatedFolder);
                if (res != -1)
                {
                    return Ok();
                }
                return BadRequest();

            }
            return BadRequest();
        }
    }
}
