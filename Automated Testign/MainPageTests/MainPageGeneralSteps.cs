using AppStartupAndRegTest;
using NUnit.Framework;
using TechTalk.SpecFlow;


namespace MainPageTests
{
    [Binding]
    public class MainPageGeneralSteps
    {

        private readonly FormContext fe;

        public MainPageGeneralSteps(FormContext FE)
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


        [Given(@"I click ""([^""]*)""")]
        public void GivenIClick(string p0)
        {
            var button = fe.GetButtonByAID(p0);
            Assert.IsNotNull(button);
            fe.WaitAndClick(button);
        }

        [Given(@"I enter ""([^""]*)"" in the ""([^""]*)"" box")]
        public void GivenIenterInTheBox(string p0, string p1)
        {
            TextBoxfill(p0, p1);
        }

        private void TextBoxfill(string p0, string p1)
        {
            var EditBox = fe.GetTextBox(p1);
            if (EditBox != null)
            {
                EditBox.Click();
                EditBox.Clear();
                EditBox.SendKeys(p0);
                var BoxValue = EditBox.Text;
                Assert.AreEqual(p0, BoxValue);
            }
            else
            {
                var CustomTextBox = fe.GetCustomTextBox(p1);
                if (CustomTextBox == null) return;
                CustomTextBox.Click();
                CustomTextBox.Clear();
                CustomTextBox.SendKeys(p0);
                var BoxValue = CustomTextBox.Text;
                Assert.AreEqual(p0, BoxValue);
            }
        }

        [Given(@"I enter (.*) in the ""([^""]*)"" boxs")]
        public void GivenIEnterInTheBox(string p0, string p1)
        {
            TextBoxfill(p0, p1);
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

        [Then(@"I get the warning about missing ""([^""]*)""")]
        public void ThenIGetTheWarningAboutMissing(string p0)
        {
            var msg = fe.GetElementByClassname(p0);
            Assert.IsNotNull(msg);
        }

        [Given(@"the ""([^""]*)"" ""([^""]*)"" is already on the list")]
        public void GivenTheIsAlreadyOnTheList(string barry, string mann)
        {
           //do nothing
        }

        [When(@"I search for list entry by name ""([^""]*)""")]
        public void WhenISearchForListEntryByName(string p0)
        {
            var msg = fe.GetElementByAccessiblyID("UserList");
            //find UserList 
            var lst = msg.FindElementsByClassName("ListBoxItem");
            var found = false;
            foreach (var entry in lst)
            {
                var textblock = entry.FindElementByAccessibilityId("UserName");
                found = (textblock.Text == p0);
                if (found) break;
            }
            Assert.IsTrue(found);
        }



        [Then(@"I have a entry on the list")]
        public void ThenIHaveAEntreyOnTheList()
        {
            //var username = fe.LastUserName;
   

           // find name on text block
        }

        [Then(@"I have the form ""([^""]*)"" open")]
        public void ThenIHaveTheFormOpen(string p0)
        {
            fe.GetFormByClassName(p0);
            Assert.IsNotNull(fe.MainForm);
        }

        [Then(@"the message is ""([^""]*)""")]
        public void ThenTheMessageIs(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the unsecessfull dialog shows")]
        public void ThenTheUnsecessfullDialogShows()
        {
            throw new PendingStepException();
        }

        [When(@"I click ""([^""]*)""")]
        public void WhenIClick(string p0)
        {
            var button = fe.GetButtonByAID(p0);
            Assert.IsNotNull(button);
            fe.WaitAndClick(button);
        }
    }


}

