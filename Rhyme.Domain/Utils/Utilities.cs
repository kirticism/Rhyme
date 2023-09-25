using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Rhyme.Domain.Utils
{
    public class Utilities
    {
        private readonly IConfiguration _configuration;

        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Dictionary<string, Uri> generateForPageURL(int pageNumber, int pageSize, string resource)
        {
            var startTimeForGenerateForPageUrl = DateTime.Now;
            var baseResource = resource + "?pageNumber={0}&pageSize={1}";
            var resp = new Dictionary<string, Uri>();
            var mainPrevURL = String.Format(baseResource, pageNumber - 1 <= 0 ? 1 : pageNumber - 1, pageSize);
            var mainNextURL = String.Format(baseResource, pageNumber + 1, pageSize);
            var baseURL = _configuration["MicroServices:NgBackend"];
            var previousPage = new Uri(baseURL + mainPrevURL);
            var nextPage = new Uri(baseURL + mainNextURL);
            resp.Add("nextPage", nextPage);
            resp.Add("prevPage", previousPage);

            var endTime = DateTime.Now;
            var executionTime = endTime - startTimeForGenerateForPageUrl;
            return resp;
        }
    }
}