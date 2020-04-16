using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace SwaggerDemo
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
            services.AddMvc();

         services.AddSwaggerGen(c =>
          {
            c.SwaggerDoc("v1",  new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "SwaggerDemo", Version = "v1" });

            c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo()
             {
                Version = "v2",
                Title = "SwaggerDemo API",
                Description = "Customers API to demo Swagger", 
                Contact=new Microsoft.OpenApi.Models.OpenApiContact()
                {
  
                    Name = "Hinault Romaric", 
                    Email = "hinault@monsite.com" 
                    
                },
                 License= new Microsoft.OpenApi.Models.OpenApiLicense(){
                      Name = "Apache 2.0"
                     
                 }
                  
                
            });

              var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SwaggerDemo.xml");

              c.IncludeXmlComments(filePath);
        
          });

          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

             app.UseSwagger();

             app.UseSwaggerUI(c =>
             {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemo v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "SwaggerDemo v2");

                 c.InjectStylesheet("/swagger-ui/css/custom.css");
             });

            app.UseMvc();
           
        }
    }
}
