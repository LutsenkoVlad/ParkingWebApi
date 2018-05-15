using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParkingCore.Interfaces;
using ParkingCore;
using ParkingCore.Enums;
using ParkingCore.Logger;

namespace ParkingRestApi
{
    public class Startup
    {
        ISettings settings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            settings = ParkingSettings.Instance;
            IConfigurationSection section = Configuration.GetSection("ParkingSettings");
            settings.SetSettings(AddPricesForCar(), section.GetValue<int>("ParkingSpace"),
                                                    section.GetValue<int>("ParkingFine"),
                                                    section.GetValue<string>("LogFilePath"),
                                                    section.GetValue<int>("Timeout"),
                                                    section.GetValue<int>("LogTimeout"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IParking>(new Parking(settings, new FileLogger(settings.LogFilePath)));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private static Dictionary<CarType,decimal> AddPricesForCar()
        {
            var prices = new Dictionary<CarType, decimal>();
            prices.Add(CarType.Passenger, 5);
            prices.Add(CarType.Truck, 3);
            prices.Add(CarType.Bus, 2);
            prices.Add(CarType.Motorcycle, 1);
            return prices;
        }
    }
}
