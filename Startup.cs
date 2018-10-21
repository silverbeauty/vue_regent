using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Regents.EntityFramework;
using Regents.Models.Courses;
using Regents.ViewModels.Courses;


namespace Regents
{
    public class Startup
    {
        private IConfigurationRoot _configuration;
        private IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment env)
        {
            
             var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

                services.Configure<Settings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDB:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDB:Database").Value;
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");    
                                }
            });
        }

        private void SafeRegisterMap<T>(Action<BsonClassMap<T>> action = null)
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                return;
            }

            BsonClassMap.RegisterClassMap<T>(cm =>
            {
                cm.AutoMap();
                action?.Invoke(cm);
            });
        }

        private void InitializeAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                //config.CreateMap<TestNewEnum, ResultFormatVM>().ReverseMap();
                config.CreateMap<Course, CourseVm>().ReverseMap();
                config.CreateMap<Unit, UnitVm>().ReverseMap();
                config.CreateMap<Topic, TopicVm>().ReverseMap();
            });
        }

        private void ConfigureMongoMapping()
        {
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);

            SafeRegisterMap<Course>(cm =>
            {
                cm.MapIdField(v => v.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            SafeRegisterMap<Unit>(cm =>
            {
                cm.MapIdField(v => v.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            SafeRegisterMap<Topic>(cm =>
            {
                cm.MapIdField(v => v.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

           
        }

/*
        //Add custom services here
         private void AddCustomServices(IServiceCollection services)
        {

            //InitializeAutoMapper();
          
            var regentsMongoDb = new MongoClient(
                    url: MongoUrl.Create(url: _configuration["Database:MongoDB:Regents"]));
                services.AddScoped<RegentsMongoContext>((serviceProvider) =>
                {
                    return new RegentsMongoContext(regentsMongoDb.GetDatabase("Regents"));
                });
		}
   */          
    }
}
