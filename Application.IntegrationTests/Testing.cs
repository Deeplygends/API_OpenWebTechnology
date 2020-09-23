using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using API;
using Application.DTOs;
using Application.Wrapper;
using Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;


namespace Application.IntegrationTests
{
    
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public void RunBeforeAnyTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();
            
            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(x =>
                x.ApplicationName == "API" && x.EnvironmentName == "Development"));

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] {"__EFMigrationsHistory"}
            };
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
        }

        public static async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<OWTDbContext>();

            var data = await context.AddAsync(entity);

            await context.SaveChangesAsync();

            return entity;
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task<TEntity> FindAsync<TEntity>(int id) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<OWTDbContext>();
            var data = await context.FindAsync<TEntity>(id);
            return data;
    
        }
    }
}
