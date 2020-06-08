using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Data
{
    public class MigratorHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly string _userName = "user@host.com";
        private readonly string _password = "1qaz@WSX";
        public MigratorHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.EnsureCreatedAsync().ConfigureAwait(false);

                _signInManager = serviceScope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                if (await context.Users.CountAsync().ConfigureAwait(false) == 0)
                    await _signInManager.UserManager.CreateAsync(new ApplicationUser
                    {
                        UserName = _userName,
                        Email = _userName,
                        EmailConfirmed = true
                    }, _password).ConfigureAwait(false);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}