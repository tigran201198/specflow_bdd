using _01ProjectStructure.Consts;
using _111ProjectStructure.Consts;
using RestSharp;
using System.Linq;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class TrelloApiActionSteps
    {
        private static IRestClient _client = new RestClient("https://api.trello.com");

        private readonly TestContext testContext;

        public TrelloApiActionSteps(TestContext testContext)
        {
            this.testContext = testContext;
        }

        private RestRequest RequestWithAuth() =>
            RequestWithoutAuth().AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        private RestRequest RequestWithoutAuth() =>
            new RestRequest();

        [Given("a request {} authorization")]
        public void ARequestWithAuthorization(bool withAuth)
        {
            testContext.Request = withAuth ? RequestWithAuth() : RequestWithoutAuth();
        }

        [Given("the request has query params:")]
        public void TheRequestHasQueryParam(Table table)
        {
            foreach (var row in table.Rows)
            {
                testContext.Request = testContext.Request.AddQueryParameter(row[0], row[1]);
            }
        }

        [Given("the request has body params:")]
        public void TheRequestHasBodyParam(Table table)
        {
            testContext.Request = testContext.Request.AddJsonBody(table.Rows.ToDictionary(row => row[0], row => row[1]));
        }

        [Given("the request has path params:")]
        public void TheRequestHasPathParam(Table table)
        {
            foreach (var row in table.Rows)
            {
                testContext.Request = testContext.Request.AddUrlSegment(row["name"], row["value"]);
            }
        }

        [When("the '{}' request is sent to '{}' endpoint")]
        public void TheRequestIsSentToGetBoardEndpoint(Method method, Endpoint endpoint)
        {
            testContext.Request.Method = method;
            testContext.Request.Resource = EndpointsProvider.GetUrl(endpoint);
            testContext.Response = _client.ExecuteAsync(testContext.Request).Result;
        }
    }
}
