using _01ProjectStructure.Consts;
using _111ProjectStructure.Utils;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly TestContext testContext;

        public Hooks(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [BeforeScenario("@CreateBoard")]
        public void CreateBoard()
        {
            var request = RestRequestProvider.RequestWithAuth()
                .AddJsonBody(new Dictionary<string, string> { { "name", "New Board" } });
            request.Method = Method.Post;
            request.Resource = BoardsEndpoints.CreateBoardUrl;
            var response = testContext.Client.ExecuteAsync(request).Result;
            testContext.BoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }
    }
}
