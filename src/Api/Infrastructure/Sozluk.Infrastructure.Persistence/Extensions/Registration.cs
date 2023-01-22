using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozluk.Infrastructure.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Sozluk.Api.Application.Interfaces.Services;
using Sozluk.Infrastructure.Persistence.Services;
using Sozluk.Api.Application.Interfaces.Token;
using Microsoft.IdentityModel.Tokens;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Infrastructure.Persistence.Repositories;

namespace Sozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SozlukContext>(options => options.UseSqlServer(configuration["ConnectionStrings"],
            option =>
            {
                option.EnableRetryOnFailure();
            }));

           //var seedData = new SeedData();
           //seedData.SeedAsync(configuration).GetAwaiter().GetResult();
            

            services.AddScoped<DbContext, SozlukContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryFavoriteRepository, EntryFavoriteRepository>();
            services.AddScoped<IEntryVoteRepository, EntryVoteRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();

            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryCommentFavoriteRepository, EntryCommentFavoriteRepository>();
            services.AddScoped<IEntryCommentVoteRepository, EntryCommentVoteRepository>();



            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHandler ,Services.Token.TokenHandler>();

            return services;
        }
    }
}
