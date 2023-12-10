using _01ProjectStructure.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class TrelloApiSteps
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

        [Given("the request has query params:")]
        public void TheRequestHasQueryParam(Table table)
        {
            foreach (var row in table.Rows)
            {
                request = request.AddQueryParameter(row[0], row[1]);
            }
        }

        [Given("the request has {string} body param with value {string}")]
        public void TheRequestHasBodyParam(string bodyParam, string value)
        {
            request = request.AddJsonBody(new Dictionary<string, string> { { bodyParam, value } });
        }

        [Given("the request has path params:")]
        public void TheRequestHasPathParam(Table table)
        {
            foreach (var row in table.Rows)
            {
                request = request.AddUrlSegment(row["name"], row["value"]);
            }
        }

        [When("the '{}' request is sent to {string} endpoint")]
        public void TheRequestIsSentToGetBoardEndpoint(Method method, string endpoint)
        {
            request.Method = method;
            request.Resource = endpoint;
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
