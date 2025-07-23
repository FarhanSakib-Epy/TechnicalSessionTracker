using EPYTST.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace EPYTST.WEB.Controllers
{
    [Route("Skill")]
    public class SkillController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public SkillController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _baseApiUrl = configuration["ApiSettings:LoginUrl"];
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
       
            return View();
        }


    }
}
