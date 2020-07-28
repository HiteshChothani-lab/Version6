using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UserManagement.Common.Utilities;
using UserManagement.Entity;
using UserManagement.Manager.Mappers;

namespace UserManagement.Manager
{
    public class LocationManager : ManagerBase, ILocationManager
    {
        public LocationManager(IConnectivity connectivity, IServiceEntityMapper mapper) : base(connectivity, mapper)
        {
        }

        private string ReadFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }

        }
        public List<CountryEntity> GetCountries()
        {
            var resourceName = "UserManagement.Manager.LocalFiles.countries.json";
            string strContent = ReadFile(resourceName);

            var countries = JsonConvert.DeserializeObject<List<CountryEntity>>(strContent);
            return countries;
        }

        public List<StateEntity> GetStates()
        {
            var resourceName = "UserManagement.Manager.LocalFiles.states.json";

            string strContent = ReadFile(resourceName);

            var states = JsonConvert.DeserializeObject<List<StateEntity>>(strContent);
            return states;
        }

        public List<CityEntity> GetCities()
        {
            var resourceName = "UserManagement.Manager.LocalFiles.cities.json";

            string strContent = ReadFile(resourceName);

            var states = JsonConvert.DeserializeObject<List<CityEntity>>(strContent);
            return states;
        }
    }
}
