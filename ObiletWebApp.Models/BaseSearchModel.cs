
namespace ObiletWebApp.Models
{
    public class BaseSearchModel
    {

        public int Page { get; set; }
        public int Limit { get; set; }
        public string SortBy { get; set; }
        public string Direction { get; set; }

    }
}
