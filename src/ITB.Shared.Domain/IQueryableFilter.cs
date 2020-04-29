using System.Linq;

namespace ITB.Shared.Domain
{
    public interface IQueryableFilter<T> where T : class
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
