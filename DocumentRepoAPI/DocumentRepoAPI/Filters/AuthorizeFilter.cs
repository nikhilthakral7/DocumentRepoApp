using DocumentRepoAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

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
        //we can't use dependency injection directly in Filters, so using Dependency resolver
        private readonly IAuthenticationService authObj = DependencyResolver.Current.GetService<IAuthenticationService>();
        private readonly int UserRole;

        public MukulAuthorizeFilter(int UserRole)
        {
            this.UserRole = UserRole;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //get token and check from db, and is active get user id  from there and the give access based on that 
            //1. Create a method to authenticate a token, input- userId, Token...output - If valid return userType, else false
            //2. Input custom string attribute in authoreize attribute,,,,match param from auth attribute and usertype got from user validation from token
            //3. 
            var req = actionContext.Request.Headers.Authorization;
            actionContext.Request.Content.ToString();
            var token = req.ToString().Split()[1];

            //Getting some error here
            var user = this.authObj.GetUserFromToken(token);
            if (user != null)
            {
                if (user.UserTypeId == UserRole)
                {
                    //Allow
                }
                else
                {
                    //Not Allow
                }
            }
            else
            {
                //Not Allow
            }

        }
    }
}