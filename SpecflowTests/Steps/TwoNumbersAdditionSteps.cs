using NUnit.Framework;
using TechTalk.SpecFlow;

namespace _18DeleteMethodValidation.Steps
{
    [Binding]
    public class TwoNumbersAdditionSteps
    {
        private int numberOne;
        private int numberTwo;
        private int result;

        [Given("user has number one as 10")]
        public void UserHasNumberOneAsTen()
        {
            numberOne = 10;
        }

        [Given("user has number two as 20")]
        public void UserHasNumberTwoAsTwenty()
        {
            numberTwo = 20;
        }

        [When("user adds number one and number two")]
        public void UserAddsNumberOneAndNumberTwo()
        {
            result = numberOne + numberTwo;
        }

        [Then("the result is 30")]
        public void TheResultIsThirty()
        {
            Assert.AreEqual(30, result);
        }
    }
}
