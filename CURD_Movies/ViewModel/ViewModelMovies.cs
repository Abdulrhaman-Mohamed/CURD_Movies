using System.ComponentModel.DataAnnotations;
using CURD_Movies.Models;

namespace CURD_Movies.ViewModel
{
    public class ViewModelMovies
    {
        public int id { get; set; }
        [Required, StringLength(250)]
        public string Name { get; set; }
        [Required, StringLength(2500)]
        public string Description { get; set; }
        
        public string? Poster { get; set; }
        [Range(1,5)]
        public double Rate { get; set; }
        [Display(Name ="Genre")]
        public byte genre_Id { get; set; }
        public IEnumerable<Genre>?  Genres { get; set; }
    }
}
