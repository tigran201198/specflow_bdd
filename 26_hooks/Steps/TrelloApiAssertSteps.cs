using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class TrelloApiAssertSteps
    {
        private readonly TestContext testContext;

        public TrelloApiAssertSteps(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Then("the response status code is {}")]
        public void TheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, testContext.Response.StatusCode);
        }

        [Then("the response matches '{}' schema")]
        public void TheResponseMatchesSchema(string schemaName)
        {
            var responseContent = JToken.Parse(testContext.Response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/" + schemaName));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Then("body value has the following values by paths:")]
        public void BodyValueByPathIsEqualTo(Table table)
        {
            foreach (var row in table.Rows)
            {
                string rowExpectedValue = row["expected_value"];
                string expectedValue = rowExpectedValue.Equals("empty_string") ? string.Empty : rowExpectedValue;
                Assert.AreEqual(expectedValue, JToken.Parse(testContext.Response.Content).SelectToken(row["path"]).ToString());
            }
        }

        [Then("the response body is equal to {string}")]
        public void TheResponseBodyIsEqualTo(string expectedValue)
        {
            Assert.AreEqual(expectedValue, testContext.Response.Content);
        }

        [Then("the response body doesn't have any item by paths:")]
        public void TheResponseBodyDoesntHaveAnyItemByPaths(Table table)
        {
            foreach (var row in table.Rows)
            {
                string rowValue = row["expected_value"];
                string expectedValue = rowValue.Equals("created_board_id") ? testContext.BoardId : rowValue;
                var responseContent = JToken.Parse(testContext.Response.Content);
                Assert.False(responseContent.Children().Select(token => token.SelectToken(row["path"]).ToString()).Contains(expectedValue));
            }
        }
    }
}
