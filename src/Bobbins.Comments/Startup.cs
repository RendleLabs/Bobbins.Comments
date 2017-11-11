using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bobbins.Comments.Data;
using Bobbins.Comments.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bobbins.Comments
{
    [UsedImplicitly]
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
            services.AddDbContextPool<CommentContext>(o => { o.UseNpgsql(Configuration.GetConnectionString("Comments")); });

            services.AddSingleton<IMapper>(_ =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Comment, CommentDto>();
                    cfg.CreateMap<CommentDto, Comment>();
                    cfg.CreateMap<Vote, VoteDto>();
                    cfg.CreateMap<VoteDto, Vote>();
                });
                
                return new Mapper(config);
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
