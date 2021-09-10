using AutoMapper;
using AutoMapper.Configuration;
using LearnEnglish.EfStuff;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace LearnEnglish
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<LearnEnglishDbContext>
                (options => options.UseSqlServer(connectionString));

            //services.AddAuthentication(AuthName)
            //    .AddCookie(AuthName, config =>
            //    {
            //        config.LoginPath = "/User/Login";
            //        config.AccessDeniedPath = "/User/Denied";
            //        config.Cookie.Name = "Smile";
            //    });

            RegisterRepositories(services);

            RegisterMapper(services);

            //services.NiceRegister<UserService>();

            //services.NiceRegister<FileService>();

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(BaseRepository<>));

            var repositories = assembly.GetTypes()
                .Where(x =>
                    x.IsClass
                    && x.BaseType.IsGenericType
                    && x.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));

            foreach (var repositoryType in repositories)
            {
                services.RegisterAssistant(repositoryType);
            }
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var provider = new MapperConfigurationExpression();

            //provider.CreateMap<BankCard, BankCardGetViewModel>();

            //provider.CreateMap<BankCardAddViewModel, BankCard>();

            //provider.CreateMap<CourseAddViewModel, Course>();

            //provider.CreateMap<RegistrationViewModel, User>();

            //provider.CreateMap<UserProfileViewModel, UserProfile>();

            var mapperConfiguration = new MapperConfiguration(provider);
            var mapper = new Mapper(mapperConfiguration);

            services.AddScoped<IMapper>(x => mapper);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}