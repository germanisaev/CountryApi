using CRUDWebAPI.Entities;
using CRUDWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRUDWebAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController: ControllerBase
    {
        private readonly ICountryService countryService;
        private readonly ILogger<CountryController> _logger;
        private readonly string WebApi = "https://api.weatherapi.com/v1/forecast.json?key=39f8ecaf506c4f76b3f55139222906&q=london&days=3&aqi=yes&alerts=yes";

        public CountryController(ILogger<CountryController> logger, ICountryService countryService)
        {
            this.countryService = countryService;
            _logger = logger;
        }


        [HttpGet("getcountrybycity")]
        public async Task<IActionResult> GetCountryByCityAsync(string city)
        {
            try
            {
                var response = await countryService.GetCountryByCityAsync(city);

                if(response == null)
                {
                    return NotFound();
                }

                return Ok(String.Format("the weatehr in {0} is: {1} condition {2} cloudy", city, response.Temp_C, response.Condition));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ICountryService.GetCountryByCityAsync encountered an exception.");
                throw;
            }
        }

        [HttpGet("getlast3days")]
        public async Task<IActionResult> GetLast3DaysAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = client.GetAsync(WebApi).Result;
                    List<object> ngo = null;
                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        ngo = JsonContent.DeserializeObject<List<object>>(response);
                    }
                    return Ok(ngo);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ICountryService.GetLast3DaysAsync encountered an exception.");
                throw;
            }
        }

        [HttpGet("getrandom")]
        public async Task<IActionResult> GetRandomNumberAsync(int num1, int num2)
        {
            try
            {
                var response = await countryService.GetRandomNumberAsync(num1, num2);

                if(response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ICountryService.GetRandomNumberAsync encountered an exception.");
                throw;
            }
        }
    }
}


