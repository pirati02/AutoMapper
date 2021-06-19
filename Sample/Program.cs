using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddMapper(Assembly.GetExecutingAssembly());

            var provider = services.BuildServiceProvider();

            var mapping = provider.GetRequiredService<IMapper>();
            var mapped = await mapping.Map<PersonOne, PersonTwo>(new PersonOne
            {
                FirstName = "dammape",
                LastName = "dzma"
            });

            Console.WriteLine(mapped.FirstName);
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonOne, PersonTwo>();
        }
    }
}