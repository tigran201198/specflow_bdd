using _01ProjectStructure.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System.IO;
using System.Net;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class GetBoardsSteps
    {
        private static IRestClient _client = new RestClient("https://api.trello.com");

        private RestRequest request;
        private RestResponse response;

        private RestRequest RequestWithAuth() =>
            RequestWithoutAuth().AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        private RestRequest RequestWithoutAuth() =>
            new RestRequest();

        [Given("a request with authorization")]
        public void ARequestWithAuthorization()
        {
            request = RequestWithAuth();
        }

        [Given("the request has fields query param")]
        public void TheRequestHasFieldsQueryParam()
        {
            request = request.AddQueryParameter("fields", "id,name");
        }

        [Given("the request has member path param")]
        public void TheRequestHasMemberPathParams()
        {
            request = request.AddUrlSegment("member", UrlParamValues.Username);
        }

        [When("the request is sent to getBoards endpoint")]
        public void TheRequestIsSentToGetBoardsEndpoint()
        {
            request.Method = Method.Get;
            request.Resource = BoardsEndpoints.GetAllBoardsUrl;
            response = _client.ExecuteAsync(request).Result;
        }

        [Then("the getBoards response status code is 200")]
        public void TheGetBoardsResponseStatusCodeIsOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then("the getBoards response matches get_boards.json schema")]
        public void TheGetBoardsResponseMatchesGetBoardsScheme()
        {
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
    }
}
