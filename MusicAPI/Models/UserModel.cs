namespace MusicAPI.Models
{
    public class UserModel
    {
        public string Name{get;set;}
        public int Age{get;set;}
        public List<string> Playlist { get; set; }
    }
}
