using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rhyme.Domain.Entities;
using Rhyme.Domain.Entities.Pagination;

namespace Rhyme.Application.Interfaces
{
    public interface ICountryRepo
    {
        public Task<object> createCountry(CountryModel countryModel);
        public Task<object> updateCountry(CountryModel countryModel);
        public Task<CountryModel> getByIdCountry(int id);
        public Task<PagedResponse<List<CountryModel>>> getAllCountry(PaginationFilter paginationFilter);
        public Task<CountryModel> deleteCountry(int id);

    }
}