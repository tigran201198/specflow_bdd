using _01ProjectStructure.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System;
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

        [Given("the request has '{word}' query param with value {string}")]
        public void TheRequestHasQueryParam(string paramName, string paramValue)
        {
            request = request.AddQueryParameter(paramName, paramValue);
        }

        [Given("the request has {string} path param with value {string}")]
        public void TheRequestHasPathParam(string paramName, string paramValue)
        {
            request = request.AddUrlSegment(paramName, paramValue);
        }

        [When("the request is sent to getBoards endpoint")]
        public void TheRequestIsSentToGetBoardEndpoint()
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

        [Then("the response status code is {}")]
        public void TheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [Then("the response matches '{}' schema")]
        public void TheResponseMatchesSchema(string schemaName)
        {
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/" + schemaName));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Then("body value by path {string} is equal to {string}")]
        public void BodyValueByPathIsEqualTo(string bodyPath, string expectedValue)
        {
            Assert.AreEqual(expectedValue, JToken.Parse(response.Content).SelectToken(bodyPath).ToString());
        }
    }
}
