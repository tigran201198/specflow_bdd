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

        [Given("the request has ID path param")]
        public void TheRequestHasIdPathParam()
        {
            request = request.AddUrlSegment("id", UrlParamValues.ExistingBoardId);
        }

        [When("the request is sent to getBoards endpoint")]
        public void theRequestIsSentToGetBoardEndpoint()
        {
            request.Method = Method.Get;
            request.Resource = BoardsEndpoints.GetAllBoardsUrl;
            response = _client.ExecuteAsync(request).Result;
        }


        [When("the request is sent to getBoard endpoint")]
        public void TheRequestIsSentToGetBoardsEndpoint()
        {
            request.Method = Method.Get;
            request.Resource = BoardsEndpoints.GetBoardUrl;
            response = _client.ExecuteAsync(request).Result;
        }

        [Then("the response status code is 200")]
        public void TheResponseStatusCodeIs()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then("the getBoards response matches get_boards.json schema")]
        public void TheGetBoardResponseMatchesGetBoardsJsonSchema()
        {
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_boards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Then("the getBoard response matches get_board.json schema")]
        public void TheGetBoardsResponseMatchesGetBoardScheme()
        {
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/get_board.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Then("body value by path name is equal to New Board")]
        public void BodyValueByPathNameIsEqualToNewBoard()
        {
            Assert.AreEqual("New Board", JToken.Parse(response.Content).SelectToken("name").ToString());
        }
    }
}
