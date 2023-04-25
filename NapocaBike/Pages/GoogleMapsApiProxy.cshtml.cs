using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace NapocaBike.Pages
{
    public class GoogleMapsApiProxyModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public GoogleMapsApiProxyModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string googleMapsApiBaseUrl = "https://maps.googleapis.com/maps/api/js";
            string apiKey = _configuration["GoogleMaps:ApiKey"];

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{googleMapsApiBaseUrl}?key={apiKey}&libraries=geometry&libraries=geocoding&callback=initMap&v=weekly");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/javascript");
                }
            }

            return NotFound();
        }
    }
}
