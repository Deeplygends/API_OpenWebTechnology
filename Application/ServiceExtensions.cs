using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Behaviours;
using Application.Policy.Handler;
using Application.Policy.Requirement;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("ContactPolicy", policy =>
                    policy.Requirements.Add(new SameUserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, ContactAuthorizationHandler>();

        }
    }
}
