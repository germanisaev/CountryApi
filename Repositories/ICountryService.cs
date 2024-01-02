using CRUDWebAPI.Entities;

namespace CRUDWebAPI.Repositories;

public interface ICountryService
{
    Task<Country> GetCountryByCityAsync(string City);
    Task<int> GetRandomNumberAsync(int num1, int num2);
}
