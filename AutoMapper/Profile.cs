using System;
using System.Collections.Generic;

namespace AutoMapper
{
    public class Profile
    {
        private readonly Dictionary<Type, Type> _profileMappings = new();

        public void CreateMap<TDestination, TSource>()
        {
            _profileMappings.TryAdd(typeof(TDestination), typeof(TSource));
            //Todo: instead of returning this create mapping class
        }

        public bool ContainsKey<TDestination>()
        {
            return _profileMappings.ContainsKey(typeof(TDestination));
        }
 
        private TDestination GetMapped<TDestination, TSource>(TSource source)
            where TDestination : new()
        {
            return PropertyMapper.MapTo<TDestination, TSource>(source);
        }
    }
}