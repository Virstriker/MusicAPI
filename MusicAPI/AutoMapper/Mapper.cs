using AutoMapper;
using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.AutoMapper
{
    public class Mapper:Profile
    {
        public Mapper() { 
            
            CreateMap<PlayListViewModel,PlaylistModel>();
            CreateMap<PlayListViewModel,PlaylistModel>().ReverseMap();
    //        CreateMap<UserViewModel, UserModel>()
    //.ForMember(dest => dest.Playlist, opt => opt.MapFrom(src => src.UserPlayList.SelectMany(list => list.Select(playlist => playlist.PlaylistName))))
    //.ReverseMap();
            //CreateMap<UserViewModel, UserModel>();
            CreateMap<UserViewModel, UserModel>().ReverseMap();
        }
        
    }
}
