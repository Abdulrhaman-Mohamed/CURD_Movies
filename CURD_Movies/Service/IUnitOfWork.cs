using CURD_Movies.Models;

namespace CURD_Movies.Service
{
    public interface IUnitOfWork:IDisposable
    {
        int Complete();
        IbaseServices<Movies> Movies { get; } 

        IbaseServices<Genre> Genres { get; }
    }
}
