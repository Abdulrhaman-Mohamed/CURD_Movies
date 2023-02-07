
using AutoMapper;
using CURD_Movies.Models;
using CURD_Movies.ViewModel;

namespace CURD_Movies.Helper
{
    public class Mappingprofile : Profile
    {
        public Mappingprofile()
        {
            // CreateMap<Movies, ViewModelMovies>();
            CreateMap<ViewModelMovies, Movies>()
                 .ForMember(dist => dist.Image_address, src => src.MapFrom(src => src.Poster))
                 .ForMember(dist => dist.genreid, src => src.MapFrom(src => src.genre_Id));

            CreateMap<ViewModelMovies, Movies>()
                 .ForMember(dist => dist.Image_address, src => src.MapFrom(src => src.Poster))
                 .ForMember(dist => dist.genreid, src => src.MapFrom(src => src.genre_Id))
                 .ReverseMap(); 


        }
    }
}
