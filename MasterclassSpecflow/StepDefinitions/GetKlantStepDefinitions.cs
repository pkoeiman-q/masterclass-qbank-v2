using System;
using TechTalk.SpecFlow;

namespace MasterclassSpecflow.StepDefinitions
{
    // Specflow tutorial video: https://www.youtube.com/watch?v=8KPrhBqZ-kk&ab_channel=ClaudioBernasconi
    [Binding]
    public class GetKlantStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public GetKlantStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }
        [Given(@"the provided Klant ID is '([^']*)'")]
        public void GivenTheProvidedKlantIDIs(string klantId)
        {
            _scenarioContext["Id"] = klantId;
        }

        [When(@"the Klanten API is called to retrieve the Klant with the ID of '([^']*)'")]
        public void WhenTheKlantenAPIIsCalledToRetrieveTheKlantWithTheIDOf(string klantId)
        {
            
        }

        [Then(@"the result should be a Klant with the ID of '([^']*)'")]
        public void ThenTheResultShouldBeAKlantWithTheIDOf(string klantId)
        {
            throw new PendingStepException();
        }
    }
}
