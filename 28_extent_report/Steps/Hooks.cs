using _01ProjectStructure.Consts;
using _111ProjectStructure.Consts;
using _111ProjectStructure.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;
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
            request.Resource = EndpointsProvider.GetUrl(Endpoint.CreateABoard);
            var response = testContext.Client.ExecuteAsync(request).Result;
            testContext.BoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }

        [AfterScenario("@DeleteBoard")]
        public void DeleteBoard()
        {
            var request = RestRequestProvider.RequestWithAuth()
                    .AddUrlSegment("id", testContext.BoardId);
            request.Method = Method.Delete;
            request.Resource = EndpointsProvider.GetUrl(Endpoint.DeleteABoard);
            var response = testContext.Client.ExecuteAsync(request).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
