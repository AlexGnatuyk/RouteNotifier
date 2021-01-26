using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RouteNotifier.Services;

namespace RouteNotifier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController
    {
        private readonly IConfiguration _configuration;

        public RouteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public string GetRouteTime(string from, string to)
        {
            var GoogleMapService = new GoogleMapsService(_configuration);
            return GoogleMapService.GetRouteTime(from, to);
        }
    }
}