using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Service2.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var client = _httpClientFactory.CreateClient("Service1");
            var result = await client.GetAsync("/");
            return Ok(result.Content);
        }
    }
}