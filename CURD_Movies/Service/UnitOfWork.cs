using CURD_Movies.Models;
using Microsoft.EntityFrameworkCore;

namespace CURD_Movies.Service
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MovieDB movieDB;

        public IbaseServices<Movies> Movies { get; private set; }
        public IbaseServices<Genre> Genres { get; private set; }
        public UnitOfWork(MovieDB dB)
        {
            movieDB = dB;
            Movies = new baseServices<Movies>(movieDB);
            Genres= new baseServices<Genre>(movieDB);
        }

        public int Complete()
        {
            return movieDB.SaveChanges();
        }

        public void Dispose()
        {
            movieDB.Dispose();
        }
    }
}
