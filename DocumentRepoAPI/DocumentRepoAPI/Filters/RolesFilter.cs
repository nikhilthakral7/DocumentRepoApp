using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DocumentRepoAPI.Filters
{
    public class RolesFilter: ActionFilterAttribute
    {
        private readonly List<string> AllowedRoles;
        public RolesFilter(string role)
        {
            this.AllowedRoles = role.Split(',').ToList();
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var currRole = actionContext.Request.Headers.GetValues("userType").FirstOrDefault();

            if (currRole == null || currRole == "")
            {
                var msg = new HttpResponseMessage();
                msg.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                msg.Content = new StringContent("Unable to determine the role of the user!");
                actionContext.Response = msg;
                return;
            }
            if (!AllowedRoles.Contains(currRole))
            {
                var msg = new HttpResponseMessage();
                msg.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                msg.Content = new StringContent("This role is not allowed!");
                actionContext.Response = msg;
                return;
            }
            base.OnActionExecuting(actionContext);

        }
    }
}