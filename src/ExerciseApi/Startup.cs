using ExerciseApi.Configuration;
using ExerciseApi.Contracts;
using ExerciseApi.Rule;
using ExternalPackage.DataAccess.Contract;
using ExternalPackage.DataAccess.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // build benefit configuration from configuration             
            var discountOptions = new DiscountOptions();
            Configuration.GetSection(DiscountOptions.Discount).Bind(discountOptions);


            var beneCostOptions = new BenefitCostOptions();
            Configuration.GetSection(BenefitCostOptions.BenefitCost).Bind(beneCostOptions);

            var payCycleOptions = new PayCycleOptions();
            Configuration.GetSection(PayCycleOptions.PayCycle).Bind(payCycleOptions);


            var benefitCostConfiguration = new BenefitCostConfiguration
            {
                PayPeriodCount = payCycleOptions.AnnualPayPeriodCount,
                AnnualEmployeeBenefitCost = beneCostOptions.AnnualEmployeeCost,
                AnnualDependentBenefitCost = beneCostOptions.AnnualDependentCost,
                DiscountByFirstName = discountOptions.ByFirstName,
                NameStartsWith = discountOptions.NameStartsWith,
                DiscountMultiplier = 1.0 - (discountOptions.DiscountPercentage * 0.01), // value in the configuration is an integer percentage value
            };

            services.AddTransient<IBenefitCostDeterminer, BenefitCostDeterminer>(s =>
            {
                return new BenefitCostDeterminer(benefitCostConfiguration);
            });

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
