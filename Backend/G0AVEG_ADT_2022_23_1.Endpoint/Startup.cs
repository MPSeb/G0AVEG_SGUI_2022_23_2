using G0AVEG_ADT_2022_23_1.Data;
using G0AVEG_ADT_2022_23_1.Endpoint.Services;
using G0AVEG_ADT_2022_23_1.Logic;
using G0AVEG_ADT_2022_23_1.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G0AVEG_ADT_2022_23_1.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IWoodLogic, WoodLogic>();
            services.AddTransient<IFurnitureLogic, FurnitureLogic>();
            services.AddTransient<IRetailerLogic, RetailerLogic>();
            services.AddTransient<IWoodRepository, WoodRepository>();
            services.AddTransient<IFurnitureRepository, FurnitureRepository>();
            services.AddTransient<IRetailerRepository, RetailerRepository>();

            services.AddTransient<FRWDbContext, FRWDbContext>();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "G0AVEG_ADT_2022_23_1.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "G0AVEG_ADT_2022_23_1.Endpoint v1"));
            }


            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors((builder) =>
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials().WithOrigins("http://localhost:7213"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
