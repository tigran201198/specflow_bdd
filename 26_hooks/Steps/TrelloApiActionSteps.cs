using _111ProjectStructure.Consts;
using _111ProjectStructure.Utils;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class TrelloApiActionSteps
    {

        private readonly TestContext testContext;

        public TrelloApiActionSteps(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Given("a request {} authorization")]
        public void ARequestWithAuthorization(bool withAuth)
        {
            testContext.Request = withAuth ? RestRequestProvider.RequestWithAuth() : RestRequestProvider.RequestWithoutAuth();
        }

        [Given("the request has query params:")]
        public void TheRequestHasQueryParam(IDictionary<string, string> rows)
        {
            foreach (var row in rows)
            {
                testContext.Request = testContext.Request.AddQueryParameter(row.Key, row.Value);
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
                string rowValue = row["value"];
                string value = rowValue.Equals("created_board_id") ? testContext.BoardId : rowValue;
                testContext.Request = testContext.Request.AddUrlSegment(row["name"], value);
            }
        }

        [When("the '{}' request is sent to '{}' endpoint")]
        public void TheRequestIsSentToGetBoardEndpoint(Method method, Endpoint endpoint)
        {
            testContext.Request.Method = method;
            testContext.Request.Resource = EndpointsProvider.GetUrl(endpoint);
            testContext.Response = testContext.Client.ExecuteAsync(testContext.Request).Result;
        }
    }
}
