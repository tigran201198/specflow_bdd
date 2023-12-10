using _01ProjectStructure.Consts;
using NUnit.Framework;
using RestSharp;

namespace _01ProjectStructure.Tests
{
    public class BaseTest
    {
        protected static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("https://api.trello.com");

        protected RestRequest RequestWithAuth(string resource, Method method) =>
            RequestWithoutAuth(resource, method).AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        protected RestRequest RequestWithoutAuth(string resource, Method method) =>
            new RestRequest(resource, method);
    }
}