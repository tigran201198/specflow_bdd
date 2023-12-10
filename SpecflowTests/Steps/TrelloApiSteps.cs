﻿using _17DeleteMethodValidation.Consts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using TechTalk.SpecFlow;

namespace _18DeleteMethodValidation.Steps
{
    [Binding]
    public class TrelloApiSteps
    {
        private static IRestClient _client = new RestClient("https://api.trello.com");

        private RestRequest request;
        private RestResponse response;

        protected RestRequest RequestWithAuth() =>
            RequestWithoutAuth().AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        protected RestRequest RequestWithoutAuth() =>
            new RestRequest();

        [Given("a request with authorization")]
        public void ARequestWithAuthorization()
        {
            request = RequestWithAuth();
        }

        [Given("a request without authorization")]
        public void ARequestWithoutAuthorization()
        {
            request = RequestWithoutAuth();
        }

        [Given("the request has query params:")]
        public void TheRequestHasQueryParams(Table table)
        {
            foreach(var row in table.Rows)
            {
                request = request.AddQueryParameter(row[0], row[1]);
            }
        }

        [Given("the request has path params:")]
        public void TheRequestHasPathParam(Table table)
        {
            foreach(var row in table.Rows)
            {
                request = request.AddUrlSegment(row["name"], row["value"]);
            }
        }

        [Given("the request has body params:")]
        public void TheRequestHasBodyParamWithValue(Table table)
        {
            request = request.AddJsonBody(table.Rows.ToDictionary(row => row[0], row => row[1]));
        }

        [When("the '{}' request is sent to {string} endpoint")]
        public void TheRequestIsSentToEndpoint(Method method, string endpoint)
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

        [Then("body value has the following values by paths:")]
        public void BodyValueByPathIsEqualTo(Table table)
        {
            foreach(var row in table.Rows)
            {
                Assert.AreEqual(row["expected_value"], JToken.Parse(response.Content).SelectToken(row["path"]).ToString());
            }
        }

        [Then("the response body is equal to {string}")]
        public void TheResponseBodyIsEqualTo(string expectedValue)
        {
            Assert.AreEqual(expectedValue, response.Content);
        }
    }
}
