using System;
using System.IO;
using AerariumTech.Pharmacy.App.Extensions;
using AerariumTech.Pharmacy.Data;
using AerariumTech.Pharmacy.Domain;
using AerariumTech.Pharmacy.App.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AerariumTech.Pharmacy.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Secrets = new ConfigurationBuilder().AddUserSecrets<Startup>().Build();
        }

        public IConfiguration Configuration { get; }
        public IConfiguration Secrets { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PharmacyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<PharmacyContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 10;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                options.AccessDeniedPath = "/Account/AccessDenied";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<MessageSenderOptions>(Secrets.GetSection("MessageSender"));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            PharmacyContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                context.Database.EnsureDeletedAsync().Wait();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            context.Database.EnsureCreatedAsync().Wait();

            roleManager.EnsureCreatedAppRolesAsync().Wait();
            userManager.EnsureCreatedDevUsersAsync().Wait();

            context.InjectData();

            // string triggers;
            // using (var sr = new StreamReader(Path.Combine(env.ContentRootPath, "Database", "Triggers.sql")))
            // {
            //     triggers = sr.ReadToEndAsync().Await();
            // }
            // context.Database.ExecuteSqlCommandAsync(triggers).Await();

            app.UseSecurityHeaders();
            app.UseXForwardedHeaders();
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}