using System;
using System.Collections.Generic;
using System.Globalization;
using FakeNewsFilter.Application.Catalog;
using FakeNewsFilter.Application.Common;
using FakeNewsFilter.Application.Mapping;
using FakeNewsFilter.Application.System;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Utilities.Constants;
using FakeNewsFilter.ViewModel.System.Users;
using FakeNewsFilter.ViewModel.Validator.News;
using FakeNewsFilter.ViewModel.Validator.Story;
using FakeNewsFilter.ViewModel.Validator.Topic;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using FakeNewsFilter.API.Controllers;
using Quartz;
using Slugify;
using StackExchange.Redis;
using Role = FakeNewsFilter.Data.Entities.Role;
using FakeNewsFilter.Utilities.Exceptions;
using FakeNewsFilter.API.Validator.News;
using FakeNewsFilter.Quartz;
using Microsoft.OpenApi.Models;

namespace FakeNewsFilter
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
            //Globalization language
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("vi")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            //Config Database Connection
            services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString)),
                ServiceLifetime.Transient);


            //AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            //Indentity
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            services.AddHttpClient();
            services.AddSwaggerGen(options =>
                   {
                       options.SwaggerDoc("v1", new OpenApiInfo
                       {
                           Title = "Template",
                           Version = "v1",
                           Description = "API template project",
                           Contact = new OpenApiContact
                           {
                               Url = new Uri("https://google.com")
                           }
                       });

                       // Add JWT authentication support in Swagger
                       var securityScheme = new OpenApiSecurityScheme
                       {
                           Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                           Name = "Authorization",
                           In = ParameterLocation.Header,
                           Type = SecuritySchemeType.Http,
                           Scheme = "bearer",
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           }
                       };

                       options.AddSecurityDefinition("Bearer", securityScheme);

                       //    var securityRequirement = new OpenApiSecurityRequirement{{securityScheme, value }};

                       //    options.AddSecurityRequirement(securityRequirement);
                   });
            //Declare DI


            services.AddTransient<FacebookAuthService>();
            services.AddTransient<UserManager<User>, UserManager<User>>();
            services.AddTransient<SignInManager<User>, SignInManager<User>>();
            services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IFollowService, FollowService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IScourceService, SourceService>();
            services.AddTransient<IStoryService, StoryService>();
            services.AddTransient<SlugHelper>();
            services.AddTransient<IExtraFeaturesService, ExtraFeaturesService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<ICloneNewsService, CloneNewsService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<VersionService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddSingleton<IConnectionMultiplexer>(GetConnectionMultiplexer());
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<INewsCommunityService, NewsCommunityService>();
            services.AddTransient<FileStorageService>();

            ConnectionMultiplexer GetConnectionMultiplexer()
            {
                var options = ConfigurationOptions.Parse(Configuration["Redis:ConnectionString"]);
                options.ConnectRetry = 5;
                options.AllowAdmin = true;

                return ConnectionMultiplexer.Connect(options);
            }

            //Fluent Validation
            //User
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<LoginRequestUserValidator>());
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestUserValidator>());
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<UpdateRequestUserValidator>());
            //Topic
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateRequestTopicValidator>());
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<UpdateRequestTopicValidator>());
            //News
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateSystemNewsValidator>());
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateOutSourceNewsValidator>());
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<UpdateSystemNewsValidator>());
            services.AddControllers().AddFluentValidation(fv =>
               fv.RegisterValidatorsFromAssemblyContaining<UpdateOutSourceNewsValidator>());
            //Story
            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateRequestStoryValidator>());

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = System.TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                    };
                });

            //Redis
            services.AddStackExchangeRedisCache(options => { options.Configuration = "localhost"; });

            //Quartz
            // if you are using persistent job store, you might want to alter some options
            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true; // default: false
                options.Scheduling.OverWriteExistingData = true; // default: true
            });

            services.AddQuartz(q =>
            {

                // handy when part of cluster or you want to otherwise identify multiple schedulers
                q.SchedulerId = "Scheduler-Core";

                // we take this from appsettings.json, just show it's possible
                q.SchedulerName = "Quartz ASP.NET Core Sample Scheduler";

                // as of 3.3.2 this also injects scoped services (like EF DbContext) without problems
                q.UseMicrosoftDependencyInjectionJobFactory();

                // or for scoped service support like EF Core DbContext
                // q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // these are the defaults
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });

                q.UseTimeZoneConverter();

                // quickest way to create a job with single trigger is to use ScheduleJob
                // (requires version 3.2)
                q.ScheduleJob<NewsController>(trigger => trigger
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
                    .RepeatForever())
                    .WithDescription("Trigger to update view count of news")
                );

                q.ScheduleJob<VoteController>(trigger => trigger
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInHours(1)
                    .RepeatForever())
                    .WithDescription("Trigger to update rate of news")
                );

                q.ScheduleJob<CloneNewsJob>(trigger => trigger
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInHours(5)
                        .RepeatForever())
                    .WithDescription("Trigger to clone news")
                );
            });

            // Quartz.Extensions.Hosting allows you to fire background service that handles scheduler lifecycle
            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/errors/{0}");
                app.UseMiddleware<MiddlewareExtentions>();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/errors/{0}");
                app.UseMiddleware<MiddlewareExtentions>();
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            using (IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<ApplicationDBContext>().Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization(app.ApplicationServices
                .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseHttpsRedirection();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}