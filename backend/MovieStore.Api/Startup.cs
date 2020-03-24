using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieStore.Core;
using MovieStore.Data;
using MovieStore.Services.Genres;
using MovieStore.Services.MovieRentals;
using MovieStore.Services.Movies;
using MovieStore.Services.Rentals;
using MovieStore.Services.Users;
using MovieStore.Services.Users.Dto;

namespace MovieStore.Api
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
            // Adicionando o Contexto da aplicação e informando que o assembly dos migrations é o Data
            services.AddDbContext<MovieStoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("MovieStore.Data")));
            // Adicionando a Dependency Injection para o UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Adicionando os Services com Dependency Injection
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IRentalService, RentalService>();
            services.AddTransient<IMovieRentalService, MovieRentalService>();
            services.AddTransient<IUserService, UserService>();
            services.AddControllers();

            // Adicionando o Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Store", Version = "v1" });
            });

            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            //Adicionando Autenticação
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Cors
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            // Configurando Autenticação
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configurando o Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Store V1");
            });

        }
    }
}
