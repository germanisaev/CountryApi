using CRUDWebAPI.Data;
using CRUDWebAPI.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebAPI.Repositories {
    public class CountryService : ICountryService
    {
        private readonly DbContextClass _dbContext;

        public CountryService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Country> GetCountryByCityAsync(string City)
        {
            var param = new SqlParameter("@City", City);

            List<Country> myResult = await Task.Run(() => _dbContext.Countries
                            .FromSqlRaw(@"exec GetCountryByCity @City", param).ToListAsync());

            //List<myClass> myResult= await _context.Set<myClass>().FromSqlRaw("CALL myStore({0});", paramId).ToListAsync<myClass>();
            return myResult.FirstOrDefault();

            //return countryDetails;
        }
    }
}


