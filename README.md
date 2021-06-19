# AutoMapper
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
