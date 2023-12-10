using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using System.IO;
using System.Net;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class TrelloApiAssertSteps : TestContext
    {
        [Then("the response status code is {}")]
        public void TheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            Assert.AreEqual(expectedStatusCode, GetResponse().StatusCode);
        }

        [Then("the response matches '{}' schema")]
        public void TheResponseMatchesSchema(string schemaName)
        {
            var responseContent = JToken.Parse(GetResponse().Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/" + schemaName));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Then("body value has the following values by paths:")]
        public void BodyValueByPathIsEqualTo(Table table)
        {
            foreach (var row in table.Rows)
            {
                Assert.AreEqual(row["expected_value"], JToken.Parse(GetResponse().Content).SelectToken(row["path"]).ToString());
            }
        }

        [Then("the response body is equal to {string}")]
        public void TheResponseBodyIsEqualTo(string expectedValue)
        {
            Assert.AreEqual(expectedValue, GetResponse().Content);
        }
    }
}
