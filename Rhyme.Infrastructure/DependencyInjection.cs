using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Rhyme.Application.Interfaces;
using Rhyme.Domain.Entities.ErrorHandling;
using Rhyme.Domain.Utils;
using Rhyme.Infrastructure.Services;

namespace Rhyme.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ICountryRepo, CountryRepo>();
            services.AddScoped<Utilities>();
            services.AddScoped<Messages>();


            return services;
        }


        public static IAsyncPolicy<HttpResponseMessage> getRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(20),
                        onRetry: (exception, retrycount, context) =>
                        {
                            Console.WriteLine(exception.Result + " " + retrycount + " " + context.PolicyKey);
                        }

                    );
        }

        public static IAsyncPolicy<HttpResponseMessage> getCircuitBreaker()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .CircuitBreakerAsync(
                        durationOfBreak: TimeSpan.FromSeconds(30),
                        handledEventsAllowedBeforeBreaking: 3
                    );
        }
    }
}