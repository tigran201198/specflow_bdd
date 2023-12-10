using RestSharp;

namespace _111ProjectStructure.Steps
{
    public class TestContext
    {
        public IRestClient Client { get; } = new RestClient("https://api.trello.com");
        public RestRequest Request { get; set; }
        public RestResponse Response { get; set; }
        public string BoardId { get; set; }
    }
}
