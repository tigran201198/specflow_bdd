using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using _01ProjectStructure.Arguments.Holders;
using _01ProjectStructure.Arguments.Providers;
using _01ProjectStructure.Consts;
using _01ProjectStructure.Tests;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace _01ProjectStructure.Tests.Create
{
    public class CreateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardNameValidationArgumentsProvider))]
        public async Task CheckCreateBoardWithInvalidName(IDictionary<string, object> bodyParams)
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(bodyParams);
            var response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);
            var errorMessage = responseContent.SelectToken("message").ToString();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("invalid value for name", errorMessage);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentsProvider))]
        public async Task CheckGetBoardWithInvalidAuth(AuthValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddJsonBody(new Dictionary<string, string> { { "name", "New item" } });
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }
    }
}