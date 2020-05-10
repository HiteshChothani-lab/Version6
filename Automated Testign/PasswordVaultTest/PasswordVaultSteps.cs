using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserManagement.Common.Constants;

namespace AutomatedTests
{
    public class PasswordVaultContext
    {
        public CurrentUserClass UserClassData;
        public MasterStore MasterData { get; set; }
        public CurrentUserClass VaultUserClassData { get; set; }
        public MasterStore VaultMasterData { get; set; }
    }

    [Binding]
    public class PasswordVaultSteps
    {
        private static PasswordVaultContext pv;

        public PasswordVaultSteps(PasswordVaultContext PV)
        {
            pv = PV;
        }

        [AfterFeature]
        public static void AfterFeature()
        {
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            Config.ClearMasterData();
            Config.ClearUserData();
        }

        public static void AreEqualByJson(object expected, object actual)
        {
            var ex = JsonConvert.SerializeObject(expected);
            var act = JsonConvert.SerializeObject(actual);

            Assert.AreEqual(ex, act);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            Config.ClearMasterData();
            Config.ClearUserData();
        }

        [Given(@"\[context]")]
        public void GivenContext()
        {
            throw new PendingStepException();
        }

        [Given(@"I save the Master data to the vault")]
        public void GivenISaveTheMasterDataToTheVault()
        {

            var storeID = pv.MasterData.StoreId.ToString();
            var json = JsonConvert.SerializeObject(pv.MasterData);
            Assert.IsTrue(Config.SaveMasterDataLocal(storeID, json));

        }

        [Given(@"I save the User data to the vault")]
        public void GivenISaveTheUserDataToTheVault()
        {
            var un = pv.UserClassData.Username;
            var json = JsonConvert.SerializeObject(pv.UserClassData);
            Assert.IsTrue(Config.SaveUserData(un, json));

        }

        [Given(@"the Master data")]
        public void GivenTheMasterData(Table table)
        {
            var Master = table.CreateInstance<MasterStore>();
            pv.MasterData = Master;
        }

        [Given(@"there is not UserClassData Data in the vault")]
        public void GivenThereIsNotUserDataInTheVault()
        {
            throw new PendingStepException();
        }

        [Given(@"the User data")]
        public void GivenTheUserData(Table table)
        {
            var user = table.CreateInstance<CurrentUserClass>();
            pv.UserClassData = user;
        }

        [Then(@"I get an null object")]
        public void ThenIGetAnNullObject()
        {
            throw new PendingStepException();
        }

        [Then(@"it is the same Master data")]
        public void ThenItIsTheSameMasterData(Table table)
        {
            var user = table.CreateInstance<MasterStore>();
            AreEqualByJson(user, pv.VaultMasterData);
        }

        [Then(@"it is the same User data")]
        public void ThenItIsTheSameUserData(Table table)
        {
            var user = table.CreateInstance<CurrentUserClass>();
            AreEqualByJson(user, pv.VaultUserClassData);
        }

        [When(@"I retrieve the Master data")]
        public void WhenIRetrieveTheMasterData()
        {

            var master = Config.MasterStore;
            pv.VaultMasterData = master;
        }

        [When(@"I retrieve the User data")]
        public void WhenIRetrieveTheUserData()
        {

            var user = Config.CurrentUser;
            Assert.AreEqual(pv.UserClassData.Username, user.Username);
            pv.VaultUserClassData = user;
        }
    }
}