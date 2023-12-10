using _111ProjectStructure.Consts;
using System;
using TechTalk.SpecFlow;

namespace _111ProjectStructure.Steps
{
    [Binding]
    public class StepArgumentsTransformer
    {
        [StepArgumentTransformation("(with|without)")]
        public bool With(string with)
        {
            return with.Equals("with");
        }

        [StepArgumentTransformation("(GetAllBoards|GetABoard|CreateABoard|DeleteABoard|UpdateABoard)")]
        public Endpoint Endpoint(string endpoint)
        {
            return (Endpoint) Enum.Parse(typeof(Endpoint), endpoint);
        }
    }
}
