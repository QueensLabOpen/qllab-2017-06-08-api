using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QLLAB.Data;
using QLLAB.Models;
using QLLAB.Repositories;
using QLLAB.Repositories.Interfaces;
using QLLAB.Services;
using QLLAB.Services.Interfaces;
using Swashbuckle.Swagger.Model;

namespace QLLAB
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SingleApiVersion(new Info
                {
                    Title = "QL LAB API",
                    Version = "v1",
                    Description = "QL LAB API"
                });
            });

            services.AddDbContext<QlLabContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            services.AddTransient<IBlobStorageImageService, BlobStorageImageService>(provider => new BlobStorageImageService(Configuration.GetConnectionString("BlobStorageConnectionString"), provider.GetService<IOptions<AppSettings>>().Value.BlobStorageContainerName));
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IQuestionService, QuestionService>();

            services.AddCors(o => o.AddPolicy("AllowAllCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowAllCorsPolicy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
