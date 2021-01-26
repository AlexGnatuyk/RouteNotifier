using System;
using System.Threading.Tasks;
using Google.Maps;
using Google.Maps.DistanceMatrix;
using Microsoft.Extensions.Configuration;

namespace RouteNotifier.Services
{
    public class GoogleMapsService
    {
        private readonly IConfiguration _configuration;

        public GoogleMapsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal string GetRouteTime(string from, string to)
        {
            var apiKey = GetApiKey() ?? throw new ArgumentNullException("GetApiKey()", "Need Google Api Key");
            var gSign = new GoogleSigned(apiKey);
            var service = new DistanceMatrixService(gSign);
            var request = GetDistanceMatrixRequest(from, to);
            var responseAsync = service.GetResponseAsync(request);
            return GetRouteTimeFromResponse(responseAsync);
        }

        private string GetRouteTimeFromResponse(Task<DistanceMatrixResponse> responseAsync)
        {
            var rows = responseAsync.GetAwaiter().GetResult().Rows[0];
            return rows.Elements[0].duration.ToString();
        }

        private DistanceMatrixRequest GetDistanceMatrixRequest(string from, string to)
        {
            var request = new DistanceMatrixRequest();
            request.AddOrigin(new Location(from));
            request.AddDestination(new Location(to));
            return request;
        }

        private string GetApiKey()
        {
            return _configuration["GoogleApiKey"];
        }
    }
}