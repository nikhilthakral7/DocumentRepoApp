using DocumentRepoWeb.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace DocumentRepoWeb.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth
        [HttpPost]
        public ActionResult Login(LoginModel userData) {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44386");
                var response = client.PostAsJsonAsync("/api/auth", userData).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var c = response.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject< LoginResponseModel>(c);

                    TempData["token"] = res.token;
                    //instead returnig view redirect to dashboard

                }
                else { 
                    //redirect to dashboard
                }
            }
                return View();
        }
    }
}