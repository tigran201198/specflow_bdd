using System;
using System.Collections;
using _01ProjectStructure.Consts;
using _01ProjectStructure.Arguments.Holders;
using RestSharp;

namespace _01ProjectStructure.Arguments.Providers
{
    public class GetBoardsAuthValidationArgumentsProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        Parameter.CreateParameter("key", UrlParamValues.ValidKey, ParameterType.QueryString)
                    },
                    ErrorMessage = "unauthorized permission requested"
                },
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = new[]
                    {
                        Parameter.CreateParameter("token", UrlParamValues.ValidToken, ParameterType.QueryString)
                    },
                    ErrorMessage = "invalid app key"
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentsHolder
                {
                    AuthParams = ArraySegment<Parameter>.Empty,
                    ErrorMessage = "unauthorized permission requested"
                }
            };
        }
    }
}