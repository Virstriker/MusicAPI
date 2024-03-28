using AutoMapper;
using MusicAPI.Dto;
using MusicAPI.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MusicAPI.Repos
{
    public class PlaylistRepo : DataBase, IPlaylistRepo
    {
        private readonly IMapper _mapper;

        public PlaylistRepo(IMapper mapper) {
            _mapper = mapper;
        }
        public int? AddPlaylist(PlaylistModel playlist)
        {
            PlaylistData.Add(playlist);
            return 1;
            if (!PlaylistData.Where(s => s.PlaylistName == playlist.PlaylistName).Any())
            {
                int? op = 1;
                int num = playlist.SongsName.Count();
                for (int i = 0; i < num; i++)
                {
                    if (!SongData.Where(s => s.SongName == playlist.SongsName[i]).Any())
                    {
                        op = null;
                    }
                }
                if (op != null)
                {
                    return op;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public PlayListViewModel GetPlayListObj(PlaylistModel Playlist)
        {
            var obj = _mapper.Map<PlayListViewModel>(Playlist);
            var List = new List<SongModel>();
            foreach (var song in Playlist.SongsName)
            {
                var temp = SongData.FirstOrDefault(c => c.SongName == song);
                if (temp != null)
                {
                    List.Add(temp);
                }
            }
            obj.SongsList = List;
            return obj;
        }
        public PlayListViewModel GetPlayListById(string name)
        {
            if(PlaylistData.Where(s=>s.PlaylistName == name).Any())
            {
                return GetPlayListObj(PlaylistData.FirstOrDefault(c => c.PlaylistName == name));
            }
            return null;
        }
        public List<PlayListViewModel> GetPlaylist()
        {
            List<PlayListViewModel> Temp = new List<PlayListViewModel>();
            int count = PlaylistData.Count;
            for (int i = 0; i < count; i++)
            {
                Temp.Add(GetPlayListObj(PlaylistData[i]));
            }
            return Temp;
        }
        public int? RemovePlayList(string playListName)
        {
            
            if (PlaylistData.Where(s => s.PlaylistName == playListName).Any())
            {
                PlaylistData.RemoveAll(s => s.PlaylistName == playListName);
                return 1;
            }
            return null;
        }
        public int? EditPlaylist(string name, PlaylistModel playlist)
        {
            if (PlaylistData.Where(s => s.PlaylistName == name).Any())
            {
                var Oldlist = PlaylistData.FirstOrDefault(s => s.PlaylistName == name);
                int count = playlist.SongsName.Count;
                if (count>=1)
                {
                    foreach (var item in playlist.SongsName) { 
                        if(SongData.Where(c => c.SongName == item).Any())
                        {
                            Oldlist.SongsName.Add(item);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                var uList = UserData.FirstOrDefault(c=>c.Playlist.Contains(name));
                if (uList != null)
                {
                UserData.Remove(uList);
                uList.Playlist.Remove(name);
                uList.Playlist.Add(playlist.PlaylistName);
                UserData.Add(uList);
                playlist.SongsName = Oldlist.SongsName;
                PlaylistData.Remove(Oldlist);
                PlaylistData.Add(playlist);
                }
                return 1;
            }
            return null;
        }
        
    }
}
