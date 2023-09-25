using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rhyme.Application.Interfaces;
using Rhyme.Domain.Entities;
using Rhyme.Domain.Entities.Pagination;

namespace Rhyme.Api.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepo _iCountryRepo;

        public CountryController(ICountryRepo iCountryRepo)
        {
            _iCountryRepo = iCountryRepo;
        }

        [HttpPost("country")]
        public async Task<IActionResult> createCountry(CountryModel countryModel)
        {
            if (countryModel.countryId != 0)
            {
                var resp = await _iCountryRepo.updateCountry(countryModel);
                var myObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(resp));
                if (myObject!.ContainsKey("error"))
                {
                    return BadRequest(resp);
                }
                return Ok(resp);
            }
            else
            {
                var resp = await _iCountryRepo.createCountry(countryModel);
                return Ok(resp);
            }
        }


        [HttpGet("country")]
        public async Task<PagedResponse<List<CountryModel>>> getAllCountry(int pageNumber, int pageSize)
        {
            var paginationFilter = new PaginationFilter(pageNumber, pageSize, HttpContext.Request.Path);
            var resp = await _iCountryRepo.getAllCountry(paginationFilter);
            return resp;
        }   


        [HttpGet("country/{id}")]
        public async Task<CountryModel> getByIdCountry(int id)
        {
            var resp = await _iCountryRepo.getByIdCountry(id);
            return resp;
        }


        [HttpDelete("country/{id}")]
        public async Task<IActionResult> deleteCountry(int id)
        {
            var resp = await _iCountryRepo.deleteCountry(id);
            return Ok(resp);
        }
        
    }
}