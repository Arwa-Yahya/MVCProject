using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class StateController : Controller
    {
        public IActionResult SetSession(string name, int age)
        {
            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetInt32("Age", age);

            return Content("Session Save Success");
        }

        public IActionResult GetSession()
        {
            string s = HttpContext.Session.GetString("Name");
            int? a = HttpContext.Session.GetInt32("Age");

            return Content($"Name = {s} - Age = {a}");
        }

        public IActionResult SetCookies(string name, int age)
        {
            CookieOptions options = new CookieOptions();
            options.Expires=DateTimeOffset.Now.AddHours(1);

            HttpContext.Response.Cookies.Append("Name", name, options);
            HttpContext.Response.Cookies.Append("Age", age.ToString(), options);

            return Content("Cookie Save Success");
        }

        public IActionResult GetCookies()
        {
            string s = HttpContext.Request.Cookies["Name"];
            string a = HttpContext.Request.Cookies["Age"];

            return Content($"Name = {s} - Age = {a}");
        }
    }
}
