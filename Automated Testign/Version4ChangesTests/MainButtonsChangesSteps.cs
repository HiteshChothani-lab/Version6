using AppStartupAndRegTest;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Version4ChangesTests
{
    [Binding]
    public class MainButtonsChangesSteps
    {
        private readonly FormContext fe;

        public MainButtonsChangesSteps(FormContext FE)
        {
            fe = FE;
        }

        [AfterFeature]
        public static void AfterFeature()
        {
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            AppiumSession.session.CloseApp();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            AppiumSession.SetUpAppiumSession();
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
        }

        [Given(@"the ""([^""]*)"" button visible ""([^""]*)""")]
        public void AndButtonVisible(string p0, string p1)
        {
            CheckButtonVisible(p0, p1);
        }

        [Given(@"I have the form ""([^""]*)"" open")]
        public void GivenIHaveTheFormOpen(string p0)
        {
            fe.GetFormByClassName(p0);
            Assert.IsNotNull(fe.MainForm);
        }

        [Given(@"I start the app")]
        public void GivenIStartTheApp()
        {
            AppiumSession.StartSession();
            Assert.IsNotNull(AppiumSession.session);
        }

        [Given(@"the ""([^""]*)"" button selected is ""([^""]*)""")]
        public void GivenTheButtonSelectedIs(string p0, string p1)
        {
            var selected = fe.ButtonSelected(p0);
            if (p1 == "True")
                Assert.IsTrue(selected);
            else
                Assert.IsFalse(selected);
        }

        [Then(@"the ""([^""]*)"" button visible ""([^""]*)""")]
        public void ThenButtonVisible(string p0, string p1)
        {
            CheckButtonVisible(p0, p1);
        }
        
        [Then(@"the (.*) button selected is ""([^""]*)""")]
        public void ThenTheButtonSelectedIs(string p0, string p1)
        {
            var selected = fe.ButtonSelected(p0);
            if (p1 == "True")
                Assert.IsTrue(selected);
            else
                Assert.IsFalse(selected);
        }

    
        [When(@"I click on the (.*)")]
        public void WhenIClickOn(string p0)

        {
            fe.ClickBtn(p0);
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