using _01ProjectStructure.Consts;
using RestSharp;

namespace _111ProjectStructure.Utils
{
    public static class RestRequestProvider
    {
        public static RestRequest RequestWithAuth() =>
            RequestWithoutAuth().AddOrUpdateParameters(UrlParamValues.AuthQueryParams);

        public static RestRequest RequestWithoutAuth() =>
            new RestRequest();
    }
}
