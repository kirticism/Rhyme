using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rhyme.Domain.Entities;

namespace Rhyme.Domain.Data
{
    public class DBContext : DbContext
    {
        // private readonly IHttpContextAccessor _httpContextAccessor;

        // protected DBContext(DbContextOptions options) : base(options)
        // {

        // }

        // // public DBContext() { }

        // protected DBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
        // {
        //     _httpContextAccessor = httpContextAccessor;
        // }

        // public DbSet<CountryModel> countryModel { get; set; }


        private readonly IHttpContextAccessor _httpContextAccessor;

        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<CountryModel> countryModel { get; set; }

    }
}