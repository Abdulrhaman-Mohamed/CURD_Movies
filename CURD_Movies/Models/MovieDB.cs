using Microsoft.EntityFrameworkCore;

namespace CURD_Movies.Models
{
    public class MovieDB :DbContext
    {
        public MovieDB(DbContextOptions<MovieDB>options):base(options)
        {

        }

        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        

    }
}
