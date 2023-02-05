using CURD_Movies.ViewModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CURD_Movies.Service
{
    public interface IbaseServices<T> where T : class
    {
        Task<T> add(T movies);

        void remove(T model);

        Task<T> get(Expression<Func<T, bool>> id);

        Task<T> getIndex(Expression<Func<T, bool>> id, string[] includes = null);

        Task<IEnumerable<T>> getList();

        Task<T> update(T movie);


    }
}
