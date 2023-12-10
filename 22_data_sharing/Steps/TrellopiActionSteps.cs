using _01ProjectStructure.Consts;
using RestSharp;
using System.Linq;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class TrellopiActionSteps : TestContext
    {
        private static IRestClient _client = new RestClient("https://api.trello.com");

        private RestRequest RequestWithAuth() =>
            RequestWithoutAuth().AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        private RestRequest RequestWithoutAuth() =>
            new RestRequest();

        [Given("a request with authorization")]
        public void ARequestWithAuthorization()
        {
            SetRequest(RequestWithAuth());
        }

        [Given("a request without authorization")]
        public void ARequestWithoutAuthorization()
        {
            SetRequest(RequestWithoutAuth());
        }

        [Given("the request has query params:")]
        public void TheRequestHasQueryParam(Table table)
        {
            foreach (var row in table.Rows)
            {
                SetRequest(GetRequest().AddQueryParameter(row[0], row[1]));
            }
        }

        [Given("the request has body params:")]
        public void TheRequestHasBodyParam(Table table)
        {
            SetRequest(GetRequest().AddJsonBody(table.Rows.ToDictionary(row => row[0], row => row[1])));
        }

        [Given("the request has path params:")]
        public void TheRequestHasPathParam(Table table)
        {
            foreach (var row in table.Rows)
            {
                SetRequest(GetRequest().AddUrlSegment(row["name"], row["value"]));
            }
        }

        [When("the '{}' request is sent to {string} endpoint")]
        public void TheRequestIsSentToGetBoardEndpoint(Method method, string endpoint)
        {
            GetRequest().Method = method;
            GetRequest().Resource = endpoint;
            SetResponse(_client.ExecuteAsync(GetRequest()).Result);
        }
    }
}
