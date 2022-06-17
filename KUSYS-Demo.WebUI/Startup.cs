using KUSYS_Demo.Business.Abstract;
using KUSYS_Demo.Business.Concrete;
using KUSYS_Demo.DataAccess.Abstract;
using KUSYS_Demo.DataAccess.Concrete.EFCore;
using KUSYS_Demo.WebUI.Identity;
using KUSYS_Demo.WebUI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace KUSYS_Demo.WebUI
{

    public  class Startup
    {

        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();



            services.Configure<IdentityOptions>(options =>
            {
                // password

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".KUSYS-Demo.Security.Cookie"
                };

            });

            services.AddScoped<IStudentDal, EFCoreStudentDal>();
            services.AddScoped<ICourseDal, EFCoreCourseDal>();
            services.AddScoped<IStudentService, StudentManager>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddMvc(options => options.EnableEndpointRouting = false);

            // IProduct, EfCoreProductDal
            // IProduct, MySqlProductDal
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }
            //app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.CustomStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "adminStudents",
                 template: "admin/students",
                 defaults: new { controller = "Admin", action = "Index" }
               );

                routes.MapRoute(
                    name: "adminStudent",
                    template: "admin/students/{id?}",
                    defaults: new { controller = "Admin", action = "Edit" }
                );
                routes.MapRoute(
                 name: "userStudents",
                 template: "user/students",
                 defaults: new { controller = "User", action = "DetailsForUser" }
               );
                routes.MapRoute(
                   name: "userStudent",
                   template: "user/students/{id?}",
                   defaults: new { controller = "User", action = "Edit" }
               );
                routes.MapRoute(
                  name: "students",
                  template: "students/{course?}",
                  defaults: new { controller = "User", action = "List" }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}"
                );

            });


            SeedIdentity.Seed(userManager, roleManager, Configuration).Wait();



        }
    }
}
