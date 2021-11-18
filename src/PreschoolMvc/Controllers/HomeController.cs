using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PreschoolMvc.Infrastructure;

namespace PreschoolMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [MyAuthorize(Users = "Bob Smith")]
        public ActionResult Authorized()
        {
            return View((User as ClaimsPrincipal));
        }
        

        [Authorize]
        public async Task<ActionResult> CallApi()
        {
           // var user = User as ClaimsPrincipal;
           // var accessToken = user.FindFirst("access_token").Value;

            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:44326/identity");
            string content;
            if (!response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                ViewBag.Json = content;
            }
            else
            {
                content = await response.Content.ReadAsStringAsync();
                ViewBag.Json = JArray.Parse(content).ToString();
            }

            return View("Json");
        }

        public ActionResult Unauthorized()
        {
            ViewBag.Message = "You are not authorized!.";

            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        //public async Task<ActionResult> CallApi()
        //{
        //    //var accessToken = await HttpContext.GetTokenAsync("access_token");
        //    //var client = new HttpClient();
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    //var content = await client.GetStringAsync("https://localhost:6001/identity");

        //    //ViewBag.Json = JArray.Parse(content).ToString();
        //    //return View("json");

        //    var user = User as ClaimsPrincipal;
        //    var token = user.FindFirst("access_token").Value;
        //    //var result = await CallApi(token);

        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var content = await client.GetStringAsync("https://localhost:44326/identity");
        //    ViewBag.Json = JArray.Parse(content).ToString();



        //    return View();
        //}

    }
}