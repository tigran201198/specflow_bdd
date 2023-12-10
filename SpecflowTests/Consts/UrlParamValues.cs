using System.Collections.Generic;
using RestSharp;

namespace _17DeleteMethodValidation.Consts
{
    public static class UrlParamValues
    {
        private const string ValidKey = "fb04999a731923c2e3137153b1ad5de0";
        private const string ValidToken = "b73120fb537fceb444050a2a4c08e2f96f47389931bd80253d2440708f2a57e1";

        public static IEnumerable<Parameter> AuthQueryParams = new[]
        {
            Parameter.CreateParameter("key", ValidKey, ParameterType.QueryString),
            Parameter.CreateParameter("token", ValidToken, ParameterType.QueryString)
        };

        public const string Username = "learnpostman";
    }
}