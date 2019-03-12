using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Database;
using Cart.API.Database.Impl;
using Cart.API.Models;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Cart.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IdGenerator>(it => new DefaultIdGenerator());
            //todo add IMongoClient
            services.AddSingleton(it => new MongoClientSettings()
            {
                Server = new MongoServerAddress("192.168.0.19", 27017)
            });

            services.AddSingleton<IMongoClient>(it => new MongoClient(it.GetService<MongoClientSettings>()));

            services.AddScoped<IMongoDatabase>(it => it.GetService<IMongoClient>().GetDatabase("CatalogApi"));

            services.AddScoped<IMongoCollection<CartModel>>(it =>
                it.GetService<IMongoDatabase>().GetCollection<CartModel>("BrandStore"));

            services.AddScoped<ICartDatabase>(it => new MongoCartDatabase(it.GetService<IMongoCollection<CartModel>>(), it.GetService<IdGenerator>()));

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
