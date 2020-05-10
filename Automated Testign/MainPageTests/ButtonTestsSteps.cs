using AppStartupAndRegTest;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MainPageTests
{
    [Binding]
    public class ButtonTestsSteps
    {

        private readonly FormContext fe;

        public ButtonTestsSteps(FormContext FE)
        {
            fe = FE;
        }
 
        [Then(@"the ""([^""]*)"" button visible ""([^""]*)""")]
        public void ThenButtonVisible(string p0, string p1)
        {
            CheckButtonVisible(p0,p1);
        }
        [Given(@"the ""([^""]*)"" button visible ""([^""]*)""")]
        public void AndButtonVisible(string p0, string p1)
        {
            CheckButtonVisible(p0, p1);
        }

        private void CheckButtonVisible(string p0, string p1)
        {
            var nd = fe.GetElementByAccessiblyID(p0);
            switch (p1)
            {
                case "False":
                    Assert.IsFalse(nd.Displayed);
                    break;
                case "True":
                    Assert.IsTrue(nd.Displayed);
                    break;
                default:
                    Assert.Fail();
                    break;
            }
        }
    }
}
