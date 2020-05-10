using NUnit.Framework;
using TechTalk.SpecFlow;
using UserManagement.Common.Constants;


namespace ConfigTest
{
    public class ClinicConfigStepsContext
    {
        public Clinic Clinic { get; set; }
        public Clinic Clinic2 { get; set; }
    }
    [Binding]
    public class ClinicConfigSteps
    {
        private ClinicConfigStepsContext CC;

        public ClinicConfigSteps(ClinicConfigStepsContext CC)
        {
            this.CC = CC;
        }

        [Then(@"the clinics are the same")]
        public void ThenTheClinicsAreTheSame()
        {
          Assert.AreEqual(CC.Clinic, CC.Clinic2);
        }

        [Given(@"I have a Clinic IO  object")]
        public void GivenIHaveAClinicIOObject()
        {
            CC.Clinic = Config.ClinicList.ClinicList[0];
        }

        [When(@"I populate the object")]
        public void WhenIPopulateTheObject()
        {
            throw new PendingStepException();
        }

        [When(@"I write out the file")]
        public void WhenIWriteOutTheFile()
        {
            ClinicReaderWriter.WriteClinic(CC.Clinic);
        }

        [When(@"I read the file")]
        public void WhenIReadTheFile()
        {
            CC.Clinic2 = Config.ClinicList.ClinicList[0];
        }

        [Then(@"the clinic type is ""([^""]*)"":")]
        public void ThenTheClinicTypeIs(string clinic)
        {
            throw new PendingStepException();
        }

        [Then(@"there are (.*) buttons")]
        public void ThenThereAreButtons(int p0, Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"I load the app")]
        public void GivenILoadTheApp()
        {
            throw new PendingStepException();
        }

        [Then(@"the buttons are")]
        public void ThenTheButtonsAre(Table table)
        {
            throw new PendingStepException();
        }
    }
}
