using EPYTST.Infrastructure.CustomException;
using EPYTST.Infrastructure.Static;
using EPYTST.WEB.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace EPYTST.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _baseApiUrl = configuration["ApiSettings:LoginUrl"];
        }

        public IActionResult Index()
        {
            TempData["User"] = "null";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LogIn logIn)
        {
            //string url = "https://localhost:44302/api/LoginUser/LogIn";
            var url = $"{_baseApiUrl}api/LoginUser/LogIn?username={logIn.UserName}&password={logIn.Password}";


            var loginRequest = new LogIn
            {
                UserName = logIn.UserName,
                Password = logIn.Password
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, jsonContent);

            var content = await response.Content.ReadAsStringAsync();

            var user = JsonSerializer.Deserialize<LoginUser>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (user.UserCode != 0) 
            {
                TempData["User"] = JsonSerializer.Serialize(user);
                return RedirectToAction("Index","Admin");
            }
            else
            {
                return View();
            }
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
