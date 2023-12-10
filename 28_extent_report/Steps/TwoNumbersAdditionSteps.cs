using NUnit.Framework;
using TechTalk.SpecFlow;

namespace _01ProjectStructure.Steps
{
    [Binding]
    public class TwoNumbersAdditionSteps
    {
        private int numberOne;
        private int numberTwo;
        private int result;

        [Given("user has number one as 10")]
        public void ARequestWithAuthorization()
        {
            numberOne = 10;
        }

        [Given("user has number two as 20")]
        public void TheRequestHasFieldsQueryParam()
        {
            numberTwo = 20;
        }

        [When("user adds number one and number two")]
        public void TheRequestIsSentToGetBoardsEndpoint()
        {
            result = numberOne + numberTwo;
        }

        [Then("the result is 30")]
        public void TheGetBoardsResponseStatusCodeIsOk()
        {
            Assert.AreEqual(result, 50);
        }
    }
}
