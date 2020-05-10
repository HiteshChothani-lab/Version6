using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace UserManagement.Common.Constants
{
    public static class ClinicReaderWriter
    {
        public static readonly string AppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TUCO");
        public static string FilePath = Path.Combine(AppPath, ClinicDatajson);
        private const string ClinicDatajson = "ClinicData.json";

        public static Clinics ReadClinic()
        {
            if (!File.Exists(FilePath)) return null;

            using (var reader = new StreamReader(FilePath, Encoding.UTF8))
            {
                var result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Clinics>(result);
            }
        }

        public static void WriteClinic(Clinic clinic)
        {
            if (!File.Exists(FilePath))
                File.Delete(FilePath);
            // update clinc
            string json = "";// = JsonConvert.SerializeObject(clinics);
            if (!Directory.Exists(AppPath))
                Directory.CreateDirectory(AppPath); // Create the Config File Exmaple folder1

            using (var outputFile = new StreamWriter(
                FilePath,
                false,
                Encoding.UTF8))
                outputFile.WriteLine(json);
        }
    }
}