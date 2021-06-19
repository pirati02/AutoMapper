using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AutoMapper.DependencyInjection
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddMapper(this IServiceCollection services, Assembly profilesAssembly,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            if (profilesAssembly == null)
            {
                throw new ArgumentNullException($"profilesAssembly parameter must be provided");
            }

            var exportedProfiles = profilesAssembly.ExportedTypes
                .Where(a =>
                    !a.IsInterface
                    && !a.IsAbstract
                    && a.BaseType == typeof(Profile)
                ).ToList();

            services.TryAdd(exportedProfiles.Select(a => new ServiceDescriptor(a, a, lifetime)));

            services.AddSingleton<IMapper>(opt => new Mapper(opt.GetRequiredService, exportedProfiles));

            return services;
        }
    }
}