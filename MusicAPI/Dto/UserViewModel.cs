using MusicAPI.Models;
namespace MusicAPI.Dto
{
    public class UserViewModel
    {
        public string Name{get;set;}
        public int Age{get;set;}
        public List<PlayListViewModel> UserPlayList { get; set; }
    }
}
