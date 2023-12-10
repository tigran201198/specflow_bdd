using RestSharp;
using System.Threading;

namespace _111ProjectStructure.Steps
{
    public class TestContext
    {
        private static readonly ThreadLocal<RestRequest> _request = new();
        private static readonly ThreadLocal<RestResponse> _response = new();

        public RestRequest GetRequest() { return _request.Value; }

        public void SetRequest(RestRequest request) { _request.Value = request;}

        public RestResponse GetResponse() { return _response.Value; }

        public void SetResponse(RestResponse response) {  _response.Value = response;}
    }
}
