using System;
using System.Linq;

namespace AutoMapper
{
    public static class PropertyMapper
    {
        public static TDestination MapTo<TDestination, TSource>(TSource source)
            where TDestination : new()
        {
            var destinationType = typeof(TDestination);
            var hasEmptyConstructor = destinationType.GetConstructors()
                .Any(a => a.GetParameters().Length == 0);

            if (!hasEmptyConstructor)
            {
                throw new InvalidOperationException(
                    $"destination type {destinationType.Name} has no empty constructor");
            }

            var destinationClass = new TDestination();
            destinationType = destinationClass.GetType();

            var sourceProperties = source.GetType().GetProperties().ToList();
            foreach (var sourceProperty in sourceProperties)
            {
                var sourcePropertyValue = sourceProperty.GetValue(source);
                var destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty == null)
                {
                    continue;
                }

                destinationProperty.SetValue(destinationClass, sourcePropertyValue);
            }

            return destinationClass;
        }
    }
}