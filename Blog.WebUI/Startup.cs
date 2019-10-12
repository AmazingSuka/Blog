using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blog.WebUI.Data;
using Blog.Persistance.Interfaces;
using Blog.Persistance.Gateways;
using MediatR;
using System.Reflection;
using Blog.Persistance;
using Blog.Persistance.DataContext;
using Blog.Domain.Interfaces;
using Blog.Domain.MongoEntities;
using Blog.Application.Posts.Commands.Create;

namespace Blog.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddCircuitOptions(o => o.DetailedErrors = true);
            services.AddMediatR(typeof(CreatePostCommandHandler).GetTypeInfo().Assembly);

            // Dependency Ijections

            // Services
            services.AddSingleton<PostService>();
            services.AddSingleton<LikeService>();
            services.AddSingleton<CommentService>();

            // DataContext is a entrypoint to Data Layer.
            services.AddSingleton<IDataContext, MongoContext>();

            // Domain Entities
            // It is avoid dependencies to Entity Emplimentations
            services.AddTransient<IPost, Post>();
            services.AddTransient<IComment, Comment>();
            services.AddTransient<ILike, Like>();

            // Settings for mongoDb instance.
            services.Configure<Settings>(options =>
            {
                options.connectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.database = Configuration.GetSection("MongoConnection:Database").Value;
                options.postCollection = Configuration.GetSection("MongoConnection:PostCollection").Value;
                options.commentCollection = Configuration.GetSection("MongoConnection:CommentCollection").Value;
                options.likeCollection = Configuration.GetSection("MongoConnection:LikeCollection").Value;
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
