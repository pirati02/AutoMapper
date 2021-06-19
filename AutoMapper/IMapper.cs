using System.Threading.Tasks;

namespace AutoMapper
{
    public interface IMapper
    {
        Task<TDestination> Map<TSource, TDestination>(TSource source)
            where TDestination : new();
    }
}