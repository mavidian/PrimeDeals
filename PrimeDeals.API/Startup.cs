using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PrimeDeals.API.SwaggerFilters;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Data.Persistence;
using PrimeDeals.Data.Repositories;
using PrimeDeals.Services;
using PrimeDeals.Services.Services;
using PrimeDeals.Services.Validatorrs.Broker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrimeDeals.API
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      //Swagger documents to generate, same as URL segment, i.e. major ApiVersion preceded by v
      //Upon launching Swagger UI, first element on the list will be displayed.
      private readonly List<string> docsToGenerate = new List<string> { "v1" };

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers()
                 .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<AddBrokerDTOValidator>());
                                          
         services.AddApiVersioning(o =>
         {
            o.ApiVersionReader = new UrlSegmentApiVersionReader();
            o.DefaultApiVersion = new ApiVersion(0, 0);
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ReportApiVersions = true;
         });

         // Register Swagger generator, defining 1 or more Swagger documents (only major ApiVersions are supported, minor must be 0)
         services.AddSwaggerGen(o =>  //based on https://dev.to/htissink/versioning-asp-net-core-apis-with-swashbuckle-making-space-potatoes-v-x-x-x-3po7
         {
            docsToGenerate.ForEach(d => o.SwaggerDoc(d, new OpenApiInfo { Title = $"PrimeDeals API {d}", Version = d }));

            o.OperationFilter<RemoveVersionFilter>();
            o.DocumentFilter<ReplaceVersionFilter>();

            o.DocInclusionPredicate((doc, desc) =>
            {  // doc is version being generated, e.g. v1; desc is the API description of a method to include/exclude in swagger documentation
               // apiVers and mapToVers below reflect ApiVersion and MapToVersion attributes on the controller and the action method respectively
               var apiVers = desc.ActionDescriptor.EndpointMetadata.OfType<ApiVersionAttribute>().SelectMany(x => x.Versions.Select(y => y.MajorVersion));
               var mapToVers = desc.ActionDescriptor.EndpointMetadata.OfType<MapToApiVersionAttribute>().SelectMany(x => x.Versions.Select(y => y.MajorVersion));
               return apiVers.Any(v => $"v{v}" == doc) && (!mapToVers.Any() || mapToVers.Any(v => $"v{v}" == doc));
            });
            // Include XML documentation files, if any are present in build output
            Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml").ToList().ForEach(f => o.IncludeXmlComments(f));
            o.EnableAnnotations();
            o.CustomSchemaIds(t => t.FullName);  //avoid schema collisions between versions
         });

         services.AddAutoMapper(typeof(MappingProfile));

         services.AddSingleton<IBrokerService, BrokerService>();
         services.AddSingleton<ISaleService, SaleService>();
         services.AddSingleton<IPolicyService, PolicyService>();

         services.AddSingleton<IUnitOfWork, UnitOfWork>();

         services.AddSingleton<IPersistor>(p => new FileSystemPersistor(AppContext.BaseDirectory + "Storage")); //uses Storage subfolder to persist data
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseSwagger();

         app.UseSwaggerUI(c =>
         {
            docsToGenerate.ForEach(d => c.SwaggerEndpoint($"/swagger/{d}/swagger.json", $"PrimeDeals API {d}"));  //e.g. "/swagger/v1/swagger.json", "PrimeDeals API v1"
            c.RoutePrefix = string.Empty;  //Swagger UI at app root instead of /swagger
         });

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
