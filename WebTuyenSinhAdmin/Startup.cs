using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Repository;
using WebTuyenSinh_Application.System;

namespace WebTuyenSinhAdmin
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
            services.AddDbContext<HeThongTuyenSinhDB>(options =>
                               options.UseSqlServer(Configuration.GetConnectionString("Web_TuyenSinh")));

            services.AddIdentity<Account, AppRole>()
                .AddEntityFrameworkStores<HeThongTuyenSinhDB>()
                .AddDefaultTokenProviders();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IExcellService, ExcellService>();
            services.AddTransient<IAdmisstionService, AdmisstionService>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddTransient<IManageMajorSevice, ManageMajorSevice>();
                 services.AddTransient<IAdmisstionService, AdmisstionService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IStatisticalService, StatisticalService>();
            services.AddTransient<IUserService, UserService>();
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder => {
                    builder.AllowAnyMethod(); builder.AllowAnyOrigin(); builder.AllowAnyHeader(); builder.WithMethods();
                }
                );
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = "Token";
                options.IdleTimeout = TimeSpan.FromMinutes(1000);
            });
            services.AddControllersWithViews();
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
          .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
