using AspNetCoreDashboard.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AspNetCoreDashboard.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment) {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginData model) {
            HttpContext.Session.SetString("CurrentUser", model.UserName ?? string.Empty);
            return View("Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // JSON data endpoint for the "JSON (custom filter expression)" dashboard
        public JsonResult GetCustomers(string countryStartsWith) {
            var jsonText = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data/customers_filter.json"));
            var result = JsonConvert.DeserializeObject<List<Customer>>(jsonText);
            if (!string.IsNullOrEmpty(countryStartsWith))
                result = result.Where(customer => customer.Country.StartsWith(countryStartsWith)).ToList();
            return Json(result);
        }
    }
}
