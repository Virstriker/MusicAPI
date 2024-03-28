using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Repos
{
    public class DataBase
    {
        public static List<ArtistModel> ArtistList = new List<ArtistModel>();
        public static List<SongModel> SongData = new List<SongModel>();
        public static List<PlaylistModel> PlaylistData = new List<PlaylistModel>();
        //public static List<PlayListViewModel> PlaylistViewData = new List<PlayListViewModel>();
        public static List<UserModel> UserData = new List<UserModel>();
    }
}
