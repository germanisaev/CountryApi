using CRUDWebAPI.Entities;

namespace CRUDWebAPI.Repositories;

public interface ICountryService
{
    Task<Country> GetCountryByCityAsync(string City);
}
