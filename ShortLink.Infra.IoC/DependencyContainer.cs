using Microsoft.Extensions.DependencyInjection;
using ShortLink.Application.Interfaces;
using ShortLink.Application.Services;
using ShortLink.Domain.Interfaces;
using ShortLink.Infra.Data.Repository;

namespace ShortLink.Infra.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILinkRepository, LinkRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILinkService, LinkService>();
            #endregion

            #region Tools
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            #endregion
        }
    }
}
