using _01ProjectStructure.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class DictionaryTransformer
    {
        [StepArgumentTransformation]
        public IDictionary<string, string> Dictionary(Table table)
        {
            var transformedDictionary = table.Rows.ToDictionary(row => row[0], row => row[1]);
            foreach (var row in transformedDictionary)
            {
                transformedDictionary[row.Key] = ConvertValue(row.Value);
            }
            return transformedDictionary;
        }

        private string ConvertValue(string value)
        {
            return value switch
            {
                "current_user_key" => UrlParamValues.ValidKey,
                "current_user_token" => UrlParamValues.ValidToken,
                "empty_value" => "",
                "another_user_key" => UrlParamValues.AnotherUserKey,
                "another_user_token" => UrlParamValues.AnotherUserToken,
                _ => value
            };
        }
    }
}
