using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace AppStartupAndRegTest
{
    public class FormContext
    {
        private WindowsElement LoginIDBox;
        public WindowsElement MainForm { get; set; }
        public WindowsElement LoginNameBox { get; set; }
        public AppiumWebElement LoginNameEditBox { get; set; }

        public void GetLoginNameBox(string LoginName)
        {
            LoginNameBox = AppiumSession.session.FindElementByAccessibilityId(LoginName);
            LoginNameEditBox = LoginNameBox.FindElementByAccessibilityId("textBox");
        }

        public void GetFormByClassName(string ClassName)
        {

            MainForm = AppiumSession.session.FindElementByClassName(ClassName);

        }

        public void GetLoginIDBox(string LoginID)
        {
            LoginIDBox = AppiumSession.session.FindElementByAccessibilityId(LoginID);
            LoginIDEditBox = LoginIDBox.FindElementByAccessibilityId("textBox");
        }

        public AppiumWebElement LoginIDEditBox { get; set; }

        public WindowsElement GetSubmitBtn()
        {
            return AppiumSession.session.FindElementByAccessibilityId("SubmitButton");
        }
        public AppiumWebElement GetCustomTextBox(string ID)
        {
            var TextBox = AppiumSession.session.FindElementByAccessibilityId(ID);
            if (TextBox.Text != "CustomTextBox")
                return null;
            var CustomTextBox = TextBox.FindElementByAccessibilityId("textBox");
            return CustomTextBox;
        }

        public WindowsElement GetTextBox(string ID)
        {
            try
            {
                var TextBox = AppiumSession.session.FindElementByAccessibilityId(ID);
                return TextBox.Text == "CustomTextBox" ? null : TextBox;
            }
            catch (OpenQA.Selenium.WebDriverException ex)
            {
                Assert.Fail();
                return null;
            }
     
        }

        public WindowsElement GetCompleteBtn()
        {
            return AppiumSession.session.FindElementByAccessibilityId("CompleteButton");
        }

        public WindowsElement GetButtonByAID(string ID)
        {
            return AppiumSession.session.FindElementByAccessibilityId(ID);
        }

        public void WaitAndClick(WindowsElement button)
        {
            System.Threading.Thread.Sleep(1000);
            button.Click();
                
        }
        public WindowsElement GetElementByAccessiblyID(string p0)
        {
            var el = AppiumSession.session.FindElementByAccessibilityId(p0);
            return el;
        }
        public WindowsElement GetElementByClassname(string p0)
        {
           var el =   AppiumSession.session.FindElementByName(p0);
           return el;
        }

        public void ClickBtn(string p0)
        {
            var btn = GetElementByAccessiblyID(p0);
            WaitAndClick(btn);
            //btn.Click();
        }

        public bool ButtonSelected(string p0)
        {
            var btn = GetElementByAccessiblyID(p0);
            return btn.Selected;
        }
    }
}