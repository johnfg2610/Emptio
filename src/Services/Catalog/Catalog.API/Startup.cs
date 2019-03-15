using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Database;
using Catalog.API.Database.Impl;
using Catalog.API.Models;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API
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
            services.AddSingleton<IdGenerator>(it => new DefaultIdGenerator());
            //todo add IMongoClient
            services.AddSingleton(it => new MongoClientSettings() {
                Server = new MongoServerAddress(Configuration.GetValue<string>("MongoLink"), Configuration.GetValue<int>("MongoPort"))
            });

            services.AddSingleton<IMongoClient>(it => new MongoClient(it.GetService<MongoClientSettings>()));

            services.AddScoped<IMongoDatabase>(it => it.GetService<IMongoClient>().GetDatabase("CatalogApi"));

            services.AddScoped<IMongoCollection<BrandModel>>(it =>
                it.GetService<IMongoDatabase>().GetCollection<BrandModel>("BrandStore"));
            services.AddScoped<IMongoCollection<ProductModel>>(it =>
                it.GetService<IMongoDatabase>().GetCollection<ProductModel>("ProductStore"));

            services.AddScoped<IBrandDatabase>(it => new MongoBrandDatabase(it.GetService<IMongoCollection<BrandModel>>(), it.GetService<IdGenerator>()));
            services.AddScoped<IProductDatabase>(it => new MongoProductDatabase(it.GetService<IMongoCollection<ProductModel>>(), it.GetService<IdGenerator>()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
    }
}
