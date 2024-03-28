using MusicAPI.Models;

namespace MusicAPI.Dto
{
    public class PlayListViewModel
    {
        public string PlaylistName { get; set; }
        public List<SongModel> SongsList { get; set; }
    }
}
