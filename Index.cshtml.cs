using IpTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace IpTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty(SupportsGet =true)]
        public string UserIp { get; set; }
        [BindProperty(SupportsGet = true)]
        public IpInfo IpTracker { get; set; } = new();

        public async Task OnGetAsync()
        {
            try
            {
                var client = new HttpClient();
            
                var requestUri = $"https://geo.ipify.org/api/v2/country,city?apiKey=at_baChvbq13PJcdBTziaX960OAFxIcW&ipAddress={UserIp}";
                var getResponse = await client.GetAsync(requestUri);
                getResponse.EnsureSuccessStatusCode();
                var contentResponse = await getResponse.Content.ReadAsStringAsync();
                var deserialIp = JsonConvert.DeserializeObject<IpInfo>(contentResponse);
                IpTracker = deserialIp;
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Location data.");
            }
        }




    }
}