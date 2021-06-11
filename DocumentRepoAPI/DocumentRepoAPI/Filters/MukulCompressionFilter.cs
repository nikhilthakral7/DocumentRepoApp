using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.IO;
using System.Net.Http;

namespace DocumentRepoAPI.Filters
{
    public class MukulCompressionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //Bypass Login action so that token should not be compressed
            if (actionExecutedContext.ActionContext.ActionDescriptor.ActionName.ToUpper() != "LOGIN")
            {
                var content = actionExecutedContext.Response.Content;
                var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;
                var zlibbedContent = bytes == null ? new byte[0] :
                CompressionHelper.GzipByte(bytes);
                actionExecutedContext.Response.Content = new ByteArrayContent(zlibbedContent);
                actionExecutedContext.Response.Content.Headers.Remove("Content-Type");
                actionExecutedContext.Response.Content.Headers.Add("Content-encoding", "gzip");
                actionExecutedContext.Response.Content.Headers.Add("Content-Type", "application/json");
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }

    public class CompressionHelper
    {
        public static byte[] GzipByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }

            using (var output = new MemoryStream())
            {
                using (var compressor = new Ionic.Zlib.GZipStream(output, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestSpeed))
                {
                    compressor.Write(str, 0, str.Length);
                }
                return output.ToArray();
            }
        }
    }
}
