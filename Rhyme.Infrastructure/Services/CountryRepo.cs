using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rhyme.Application.Interfaces;
using Rhyme.Domain;
using Rhyme.Domain.Data;
using Rhyme.Domain.Entities;
using Rhyme.Domain.Entities.ErrorHandling;
using Rhyme.Domain.Entities.Pagination;
using Rhyme.Domain.Utils;

namespace Rhyme.Infrastructure.Services
{
    public class CountryRepo : ICountryRepo
    {
        private readonly DBContext _context;
        private readonly Utilities _utilities;
        private readonly Messages _messages;

        public CountryRepo(DBContext context, Utilities utilities, Messages messages)
        {
            _context = context;
            _utilities = utilities;
            _messages = messages;
        }

        public async Task<object> createCountry(CountryModel countryModel)
        {
            var existingCountry = await _context.countryModel
                .Where(c => c.countryName == countryModel.countryName && c.isDeleted == true)
                .FirstOrDefaultAsync();

            if (existingCountry != null)
            {
                if (existingCountry.isDeleted == true)
                {
                    if (countryModel.fromDate!.Value.Date > DateTime.UtcNow.Date)
                    {
                        countryModel.isActive = false;
                    }
                    else if (countryModel.fromDate.Value.Date == DateTime.UtcNow.Date && countryModel.isActive == false)
                    {
                        countryModel.isActive = true;
                    }
                    else
                    {
                        countryModel.isActive = true;
                    }

                    _context.Entry(existingCountry).CurrentValues.SetValues(countryModel);
                    await _context.SaveChangesAsync();
                    return existingCountry;
                }
                else
                {
                    var errorObject = new Dictionary<string, object>();
                    errorObject.Add("error", "Duplicate Country Name");
                    var errorJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(errorObject));
                    return errorJson!;
                }
            }
            else
            {
                if (countryModel.fromDate!.Value.Date > DateTime.UtcNow.Date)
                {
                    countryModel.isActive = false;
                }
                else if (countryModel.fromDate.Value.Date == DateTime.UtcNow.Date && countryModel.isActive == false)
                {
                    countryModel.isActive = true;
                }
                else
                {
                    countryModel.isActive = true;
                }

                var resp = _context.countryModel.AddAsync(countryModel);
                await _context.SaveChangesAsync();
                return countryModel;
            }
        }

        public async Task<CountryModel> deleteCountry(int id)
        {
            var startTimeForChannel = DateTime.Now;
            var countryModel = _context.countryModel.Where(a => a.countryId == id).FirstOrDefault();

            if (countryModel!.isUsed == false || countryModel.isUsed == null)
            {
                if (countryModel != null)
                {
                    if (countryModel.isDeleted == true)
                    {
                        throw new Exception("record does not exist");
                    }
                    countryModel.isActive = false;
                    countryModel.isDeleted = true;
                    countryModel.updatedDate = DateTime.UtcNow;

                    await _context.SaveChangesAsync();

                    var oldData = await _context.countryModel.FindAsync(id);
                    _context.Entry(oldData).CurrentValues.SetValues(countryModel);

                    await _context.SaveChangesAsync();

                    return countryModel;
                }
                else
                {
                    throw new Exception(_messages.eleNotFound);
                }
            }
            else
            {
                throw new Exception("Cannot delete used record");
            }
        }

        public async Task<PagedResponse<List<CountryModel>>> getAllCountry(PaginationFilter paginationFilter)
        {
            var startTimeForChannel = DateTime.Now;
            var pageRespDic = _utilities.generateForPageURL(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.applicationPath);
            var resp = await _context.countryModel.Where(s => s.isDeleted == false).OrderBy(a => a.countryName).Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToListAsync();
            var totalRecords = (from c in _context.countryModel where c.isDeleted == false select c).Count();
            var previousPage = paginationFilter.PageNumber - 1 <= 0 ? 0 : paginationFilter.PageNumber - 1;
            var testResp = new PagedResponse<List<CountryModel>>(resp, paginationFilter.PageNumber, paginationFilter.PageSize, pageRespDic["prevPage"], pageRespDic["nextPage"], totalRecords);
            return testResp;
        }

        public async Task<CountryModel> getByIdCountry(int id)
        {
            var resp = await (from c in _context.countryModel where c.countryId == id && c.isDeleted == false select c).ToListAsync();
            if (resp.Count > 0)
            {
                return resp[0];
            }
            else
            {
                throw new Exception(_messages.eleNotFound);
            }
        }

        public async Task<object> updateCountry(CountryModel countryModel)
        {
            var startTimeForChannel = DateTime.Now;
            int id = countryModel.countryId;
            countryModel.updatedDate = DateTime.UtcNow;


            if (countryModel.fromDate > DateTime.UtcNow)
            {
                countryModel.isActive = false;
            }
            else
            {
                countryModel.isActive = true;
            }

            var oldProduct = await _context.countryModel.FindAsync(id);
            _context.Entry(oldProduct).CurrentValues.SetValues(countryModel);
            await _context.SaveChangesAsync();
            var endTime = DateTime.Now;
            var executionTime = endTime - startTimeForChannel;

            return countryModel;
        }
    }
}