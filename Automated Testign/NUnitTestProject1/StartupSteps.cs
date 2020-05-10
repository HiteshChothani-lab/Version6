using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;
using UserManagement.Common.Constants;

namespace AppStartupAndRegTest
{
    public struct setupConst
    {
        public const string AppPath = @"C:\tuco\main\";
        public const string UriPath = "http://127.0.0.1:4723";
        public const string VersionString = "Version4-";
        public const string WpfAppName = @"UserManagement.exe";
        //todo need to consolidate the file names
    }

    public static class AppiumSession
    {
        public static WindowsDriver<WindowsElement> session { get; set; }

        public static AppiumOptions AppiumOptions { get; set; }
        public static string MasterStoreFile { get; set; }
        public static string UserStoreFile { get; set; }
        public static string AppFile { get; set; }
        public static string FilePath { get; set; }
        public static Uri URI { get; internal set; }

        public static void StartSession()
        {
            AppiumSession.session = new WindowsDriver<WindowsElement>(AppiumSession.URI, AppiumSession.AppiumOptions);
            AppiumSession.session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
           // AppiumSession.session.Manage().Window.Maximize();
        }

        public static void SetUpAppiumSession()
        {
            AppiumSession.URI = new Uri(setupConst.UriPath);
            AppiumSession.AppFile = Path.Combine(setupConst.AppPath, setupConst.WpfAppName);
            AppiumSession.FilePath = setupConst.AppPath;
            AppiumSession.AppiumOptions = new AppiumOptions();
            AppiumSession.AppiumOptions.AddAdditionalCapability("app", AppiumSession.AppFile);
            AppiumSession.AppiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
        }
    }

    [Binding]
    public class StartupSteps
    {
        private readonly FormContext fe;
        

        public StartupSteps(FormContext FE)
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

        [Then(@"I start the app")]
        public void ThenIStartTheApp()
        {
            AppiumSession.StartSession();
            Assert.IsNotNull(AppiumSession.session);
        }
        
        [Given(@"I clear the Maste store json file")]
        public void GivenIClearTheMasteStoreJsonFile()
        {
            Config.ClearMasterData();
        }

        [Given(@"I clear the valid user Json file")]
        public void GivenIClearTheValidUserJsonFile()
        {
            Config.ClearUserData();
        }

        [Given(@"I enter a invalid name")]
        public void GivenIEnterAInvalidName()
        {
            throw new PendingStepException();
        }

        [Given(@"I enter a valid id")]
        public void GivenIEnterAValidId()
        {
            throw new PendingStepException();
        }

        [Given(@"I enter a valid ID")]
        public void GivenIEnterAValidID()
        {
            throw new PendingStepException();
        }

        [Given(@"I enter a valid name")]
        public void GivenIEnterAValidName()
        {
            throw new PendingStepException();
        }

        [Given(@"I enter ""([^""]*)"" in address and ""([^""]*)""")]
        public void GivenIEnterInAddressAnd(int p0, string p1)
        {
            throw new PendingStepException();
        }
        [Given(@"I leave the default values in place")]
        public void GivenILeaveTheDefaultValuesInPlace()
        {
            // do nothing
        }

        [Given(@"I enter ""([^""]*)"" in Country")]
        public void GivenIEnterInCountry(string p0)
        {
          var textbox =  fe.GetTextBox("Country");
            Assert.IsNotNull(fe.LoginIDEditBox);
            textbox.Clear();
            var c = textbox;
            var cty = c.FindElementByName(p0);

            Assert.AreEqual(p0, cty.Text);
        }

        [Given(@"I enter ""([^""]*)"" in phone number")]
        public void GivenIEnterInPhoneNumber(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"I enter ""([^""]*)"" in Postal code")]
        public void GivenIEnterInPostalCode(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"I enter ""([^""]*)"" in Province")]
        public void GivenIEnterInProvince(string ontario)
        {
            throw new PendingStepException();
        }

        [Given(@"I enter ""([^""]*)"" in store")]
        public void GivenIEnterInStore(string p0)
        {
            throw new PendingStepException();
        }

        [Given(@"I enter ""([^""]*)"" in the ID")]
        public void GivenIEnterInTheID(string p0)
        {
            fe.GetLoginIDBox("LoginID");
            Assert.IsNotNull(fe.LoginIDEditBox);
            fe.LoginIDEditBox.Clear();
            fe.LoginIDEditBox.SendKeys(p0);
            var boxvalue = fe.LoginIDEditBox.Text;
            Assert.AreEqual(p0, boxvalue);
        }

        [Given(@"I enter ""([^""]*)"" in the name")]
        public void GivenIEnterInTheName(string p0)
        {
            fe.GetLoginNameBox("LoginName");
            Assert.IsNotNull(fe.LoginNameBox);
            fe.LoginNameEditBox.Clear();
            fe.LoginNameEditBox.SendKeys(p0);
            var boxvalue = fe.LoginNameEditBox.Text;
            Assert.AreEqual(p0, boxvalue);
        }

        [Given(@"I enter ""Ottawa in City")]
        public void GivenIEnterOttawaInCity()
        {
            throw new PendingStepException();
        }

        [Given(@"I have the form ""([^""]*)"" open")]
        public void GivenIHaveTheFormOpen(string p0)
        {
            fe.GetFormByClassName(p0);
            Assert.IsNotNull(fe.MainForm);
        }

        [Then(@"I have the form ""([^""]*)"" open")]
        public void ThenIHaveTheFormOpen(string p0)
        {
            fe.GetFormByClassName(p0);
            Assert.IsNotNull(fe.MainForm);

        }

        [Given(@"I select Eastern time")]
        public void GivenISelectEasternTime()
        {
            throw new PendingStepException();
        }


        [Then(@"I see the invalid postal code message")]
        public void ThenISeeTheInvalidPostalCodeMessage()
        {
            throw new PendingStepException();
        }

        [Then(@"the application closes without any message")]
        public void ThenTheApplicationClosesWithoutAnyMessage()
        {
            throw new PendingStepException();
        }

        [Then(@"the city is ""([^""]*)""")]
        public void ThenTheCityIs(string ottawa)
        {
            throw new PendingStepException();
        }

        [Then(@"the country is ""([^""]*)""")]
        public void ThenTheCountryIs(string canada)
        {
            throw new PendingStepException();
        }

        [Then(@"the postal code is ""([^""]*)""")]
        public void ThenThePostalCodeIs(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the provence is ""([^""]*)""")]
        public void ThenTheProvenceIs(string ontario)
        {
            throw new PendingStepException();
        }

        [Then(@"the time zone is ""([^""]*)""")]
        public void ThenTheTimeZoneIs(string eastern)
        {
            throw new PendingStepException();
        }

        [When(@"I change the city to ""([^""]*)""")]
        public void WhenIChangeTheCityTo(string toronto)
        {
            throw new PendingStepException();
        }

        [When(@"I click back")]
        public void WhenIClickBack()
        {
            throw new PendingStepException();
        }

        [When(@"I click Complete regisration")]
        public void WhenIClickCompleteRegisration()
        {
            var btn = fe.GetCompleteBtn();
            Assert.IsNotNull(btn);
            btn.Click();

        }

        [When(@"I click Submit")]
        public void WhenIClickSubmit()
        {
            var btn = fe.GetSubmitBtn();
            Assert.IsNotNull(btn);
            btn.Click();
        }
    }
}