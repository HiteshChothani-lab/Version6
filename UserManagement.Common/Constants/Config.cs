using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace UserManagement.Common.Constants
{

    public struct Keys
    {
        public const string SymmetricKey = "723FFEB59C2BB844";
    }

    public interface IConfig
    {
        CurrentUserClass CurrentUserClass { get; }

        MasterStore MasterStore { get; }

        void ClearMasterData();

        void ClearUserData();

        bool SaveMasterData(string storeID, string data);

        bool SaveUserData(string UserName, string json);

    }

    public static class Config
    {
        private const string MasterStoreJson = "master-store.data";
        private const string ValidatedUserJson = "validated-user.data";
        private const string Version4 = "Version4-";

        public static readonly string AppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TUCO");


        //public string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Version4);
        public static string FilePath = Path.Combine(AppPath, Version4);


        public static CurrentUserClass CurrentUser => _currentUser ?? (_currentUser = GetLocalUser());

        public static MasterStore MasterStore => _masterStore ?? (_masterStore = GetLocalMasterStore());

        public static Clinics ClinicList => _clinics ?? (_clinics = ClinicReaderWriter.ReadClinic());

        public static Clinic CurClinic => _curClinic??(_curClinic =  ClinicList.ClinicList.FirstOrDefault(t => t.ClinicType == Config.MasterStore.FacilityType));

        private static CurrentUserClass _currentUser = GetLocalUser();
        private static MasterStore _masterStore;
        private static Clinics _clinics;
        private static Clinic _curClinic;


        public static void ClearMasterData()
        {
            var MasterFile = FilePath + MasterStoreJson;
            if (File.Exists(MasterFile))
                File.Delete(MasterFile);
        }

        public static void ClearUserData()
        {
            var UserFile = FilePath + ValidatedUserJson;
            if (File.Exists(UserFile))
                File.Delete(UserFile);
        }


        public static bool SaveMasterDataLocal(string storeID, string data)
        {
            //var data = CryptoEngine.Encrypt(data, Keys.SymmetricKey);
            var json = data;

            // Does the config folder not exist?
            if (!Directory.Exists(AppPath))
                Directory.CreateDirectory(AppPath); // Create the Config File Exmaple folder1

            //todo need to consolidate the file names
            using (var outputFile = new StreamWriter($"{FilePath}{MasterStoreJson}", false, Encoding.UTF8))
                outputFile.WriteLine(json);

            if (FilePath != null)
                File.SetAttributes($"{FilePath}{MasterStoreJson}", FileAttributes.Hidden);

            return true;
        }

    public static bool SaveUserData(string UserName, string data)
    {
        //            data = CryptoEngine.Encrypt(data, Keys.SymmetricKey);
        var json = data;
        try
        {
            // Does the config folder not exist?
            if (!Directory.Exists(AppPath))
                Directory.CreateDirectory(AppPath); // Create the Config File Exmaple folder1

            using (var outputFile = new StreamWriter(
                FilePath + ValidatedUserJson,
                false,
                Encoding.UTF8))
                outputFile.WriteLine(json);

            //File.SetAttributes(FilePath + ValidatedUserJson, FileAttributes.Hidden);
            return true;
        }
        catch (IOException)
        {
            return false;
        }
    }

    private static MasterStore GetLocalMasterStore()
    {
        var masterPath = FilePath + MasterStoreJson;
        if (!File.Exists(masterPath)) return null;

        using (var reader = new StreamReader(masterPath, Encoding.UTF8))
        {
            var result = reader.ReadToEnd();
            //result = CryptoEngine.Decrypt(result, Keys.SymmetricKey);
            return JsonConvert.DeserializeObject<MasterStore>(result);
        }
    }

    private static CurrentUserClass GetLocalUser()
    {
        var userPath = FilePath + ValidatedUserJson;
        if (!File.Exists(userPath)) return null;
        using (var reader = new StreamReader(userPath, Encoding.UTF8))
        {
            var result = reader.ReadToEnd();
            //result = CryptoEngine.Decrypt(result, Keys.SymmetricKey);
            return JsonConvert.DeserializeObject<CurrentUserClass>(result);
        }
    }
}

public class CurrentUserClass
{
    public string AccessCode { get; set; }
    public string AppVersionName { get; set; }
    public string Messagee { get; set; }
    public string Status { get; set; }
    public string Token { get; set; }
    public string UserId { get; set; }
    public string Username { get; set; }
}

public class MasterStore
{
    public string Address { get; set; }
    public string Country { get; set; }
    public long CountryCode { get; set; }
    public string Messagee { get; set; }
    public string Phone { get; set; }
    public string PostalCode { get; set; }
    public string Status { get; set; }
    public long StoreId { get; set; }
    public string StoreName { get; set; }
    public string Street { get; set; }
    public long SuperMasterId { get; set; }
    public TimeZoneInfo TimeZone { get; set; }
    public long UserId { get; set; }
    public string FacilityType { get; set; }
}
}