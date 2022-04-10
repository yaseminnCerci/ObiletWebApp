using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
namespace ObiletWebApp.UII.Views
{
    public class BusJourney : PageModel
    {
        private readonly ILogger<BusJourney> _logger;

        public BusJourney(ILogger<BusJourney> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
