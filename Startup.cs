using AutoMapper;
using AutoMapper.Configuration;
using LearnEnglish.EfStuff;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.EfStuff.Repositories.IRepository;
using LearnEnglish.Models.BankCard;
using LearnEnglish.Models.Book;
using LearnEnglish.Models.Lesson;
using LearnEnglish.Models.Test;
using LearnEnglish.Models.User;
using LearnEnglish.Models.UserProfile;
using LearnEnglish.Services;
using LearnEnglish.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace LearnEnglish
{
    public class Startup
    {
        public const string AuthName = "LearnEnglishCoockie";

        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<LearnEnglishDbContext>
                (options => options.UseSqlServer(connectionString));

            services.AddAuthentication(AuthName)
                .AddCookie(AuthName, config =>
                {
                    config.LoginPath = "/User/Login";
                    config.AccessDeniedPath = "/User/Denied";
                    config.Cookie.Name = "Smile";
                });

            #region registration of interfeces for repositories

            services.AddScoped<IBankCardRepository>(x => new BankCardRepository(x.GetService<LearnEnglishDbContext>()));

            services.AddScoped<ILessonRepository>(x => new LessonRepository(x.GetService<LearnEnglishDbContext>()));

            services.AddScoped<ITestRepository>(x => new TestRepository(x.GetService<LearnEnglishDbContext>()));

            services.AddScoped<IUserProfileRepository>(x => new UserProfileRepository(x.GetService<LearnEnglishDbContext>()));

            services.AddScoped<IUserRepository>(x => new UserRepository(x.GetService<LearnEnglishDbContext>()));

            services.AddScoped<IUserService>(x => new UserService(x.GetService<IUserRepository>(), x.GetService<IHttpContextAccessor>()));

            services.AddScoped<IFileService>(x => new FileService(x.GetService<IWebHostEnvironment>()));

            services.AddScoped<IBookRepository>(x => new BookRepository(x.GetService<LearnEnglishDbContext>()));

            #endregion

            RegisterRepositories(services);

            RegisterMapper(services);

            services.RegisterAssistant<UserService>();

            services.RegisterAssistant<FileService>();

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
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

            //Who am I?
            app.UseAuthentication();

            //Waht can I see?
            app.UseAuthorization();

            app.UseMiddleware<LocalizeMidlleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
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
                services.RegisterAssistant( repositoryType);
            }
        }

        private static void RegisterMapper(IServiceCollection services)
        {
            var provider = new MapperConfigurationExpression();

            provider.CreateMap<BankCard, BankCardGetViewModel>();

            provider.CreateMap<BankCardAddViewModel, BankCard>();

            provider.CreateMap<BankCard, BankCardGetViewModel>();

            provider.CreateMap<LessonViewModel, Lesson>();

            provider.CreateMap<TestAddViewModel, Test>();

            provider.CreateMap<RegistrationViewModel, User>();

            provider.CreateMap<UserProfileViewModel, UserProfile>();

            provider.CreateMap<UserProfile, UserProfileViewModel>();

            provider.CreateMap<BookViewModel, Book>();
            
            provider.CreateMap<Book, BookViewModel>();

            provider.CreateMap<User, UserViewModel>()
                .ForMember(
                    nameof(UserViewModel.FirstName),
                    config => config.MapFrom(user => user.UserProfile.FirstName))
                .ForMember(
                    nameof(UserViewModel.LastName),
                    config => config.MapFrom(user => user.UserProfile.LastName));


            var mapperConfiguration = new MapperConfiguration(provider);
            var mapper = new Mapper(mapperConfiguration);

            services.AddScoped<IMapper>(x => mapper);
        }        
    }
}