using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CURD_Movies.Models
{
    public class Movies
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public byte id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(2500)]
        public string Description { get; set; }
        
        public string Image_address { get; set; }

        public double Rate { get; set; }
        
        public byte genreid { get; set; }

        public Genre Genre { get; set; }




    }
}
