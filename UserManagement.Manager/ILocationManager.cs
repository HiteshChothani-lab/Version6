using System.Collections.Generic;
using UserManagement.Entity;

namespace UserManagement.Manager
{
    public interface ILocationManager
    {
        List<CountryEntity> GetCountries();
        List<StateEntity> GetStates();
        List<CityEntity> GetCities();

    }
}