using CURD_Movies.Models;
using CURD_Movies.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CURD_Movies.Service
{
    public class baseServices<T> : IbaseServices<T> where T : class
    {
        private readonly MovieDB Dbcontext;
        public baseServices(MovieDB dbcontext)
        {
            Dbcontext = dbcontext;
        }

        public async Task<T> add(T movies)
        {
            await Dbcontext.Set<T>().AddAsync(movies);
            await Dbcontext.SaveChangesAsync();
            return movies;
        }

        public async Task<T> get(Expression<Func<T, bool>> id)
        {
            return await Dbcontext.Set<T>().FirstOrDefaultAsync(id);
        }

        public async Task<T> getIndex(Expression<Func<T, bool>> id, string[] includes = null)
        {
            IQueryable<T> query=Dbcontext.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query=query.Include(include);
               
            return await query.FirstOrDefaultAsync(id);
        }

        public async Task<IEnumerable<T>> getList()
        {
            return await Dbcontext.Set<T>().ToListAsync();
        }

        public  void remove(T model)
        {
            Dbcontext.Set<T>().Remove(model);
            Dbcontext.SaveChanges();
        }

        public async Task<T> update(T movie)
        {
            Dbcontext.Set<T>().Update(movie);
            Dbcontext.SaveChanges();
            return movie;
        }
    }
}
