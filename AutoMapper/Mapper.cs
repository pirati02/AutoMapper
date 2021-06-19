using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AutoMapper
{
    public class Mapper : IMapper
    {
        private readonly Func<Type, object> _service;
        private readonly IList<Type> _profileTypes;

        public Mapper(Func<Type, object> service, IList<Type> profileTypes)
        {
            _service = service;
            _profileTypes = profileTypes;
        }

        public Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            var sourceType = source.GetType();
            var profileType = _profileTypes.SingleOrDefault(type =>
            {
                var instance = _service(type);
                var containsMethodInfo =
                    type.GetMethod("ContainsKey", BindingFlags.Instance | BindingFlags.Public);
                var containsGenericMethod = containsMethodInfo?.MakeGenericMethod(sourceType);

                var containsMethod = containsGenericMethod?.Invoke(instance, null);
                bool.TryParse(containsMethod?.ToString(), out bool result);
                return result;
            });

            if (profileType == null)
            {
                throw new ArgumentNullException($"matching profile not found for type {sourceType.Name}");
            }

            var profile = _service(profileType);
            var profileMappedMethod =
                profileType.BaseType?.GetMethod("GetMapped", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.MakeGenericMethod(typeof(TDestination), sourceType);
            var value = profileMappedMethod?.Invoke(profile, new[] {(object) source});
            return Task.FromResult((TDestination)value);
        }
    }
}