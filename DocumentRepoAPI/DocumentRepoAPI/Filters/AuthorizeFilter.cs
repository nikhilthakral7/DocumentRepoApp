using DocumentRepoAPI.Services.Implementations;
using DocumentRepoAPI.Services.Interfaces;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Unity;

namespace DocumentRepoAPI.Filters
{
    //public class MukulAuthorizeFilter : IAuthorizationFilter
    //{
    //    public bool AllowMultiple => throw new NotImplementedException();

    //    public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
    //    {
    //        //actionContext.Request.Headers.Authorization;
    //        //get token and check from db, and is active get user id  from there and the give access based on that 
    //    }
    //}

    public class MukulAuthorizeFilter : AuthorizationFilterAttribute
    {
        [Dependency]
        public IAuthenticationService authObj { get; set; }


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //get token and check from db, and is active get user id  from there and the give access based on that 

            var req = actionContext.Request.Headers.Authorization;
            var token = req.ToString().Split()[1];

            var output = authObj.ValidateToken(token);
            if (output.status)
            {
                //Allow
                actionContext.Request.Headers.Add("userId", output.userId.ToString());
                actionContext.Request.Headers.Add("userType", output.userRole);
            }
            else
            {
                //Not Allow
                var msg = new HttpResponseMessage();
                msg.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                msg.Content = new StringContent(output.message);
                //Setting response will return back, instead of going forward to controller
                actionContext.Response = msg;
            }
            base.OnAuthorization(actionContext);

        }
    }
}