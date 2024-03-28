using AutoMapper;
using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Repos
{
    public class UserRepo :DataBase, IUserRepo
    {
        private readonly IMapper _mapper;
        public UserRepo(IMapper mapper) { 
            _mapper = mapper;
        }
        public int? AddUser(UserModel User)
        {

            if(!UserData.Where(s=>s.Name == User.Name).Any())
            {
                int? op = 1;
                int count = User.Playlist.Count;
                for(int i = 0; i < count; i++)
                {
                    if (!PlaylistData.Where(s => s.PlaylistName == User.Playlist[i]).Any()) {
                        op = null;
                    }
                }
                if(op != null) { 
                    UserData.Add(User);
                }
                return op;
            }
            return null;
        }

       

        public UserViewModel GetListOfUserList(UserModel user)
        {
            var obj = _mapper.Map<UserViewModel>(user);
            var list = new List<PlayListViewModel>();
            foreach (var item in user.Playlist) {
                var temp = PlaylistData.FirstOrDefault(c => c.PlaylistName == item);
                if(temp != null)
                {
                    list.Add(GetPlayListObj(temp));
                }
            }
            obj.UserPlayList = list;
            return obj;
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
        public UserViewModel GetUserById(string name)
        {
            if(UserData.Where(s => s.Name == name).Any())
            {
                return GetListOfUserList(UserData.FirstOrDefault(c => c.Name == name));
            }
            return null;
        }
        public List<UserViewModel> GetUserList()
        {
            var Temp = new List<UserViewModel>();
            int? count = UserData.Count;
           
            for (int i = 0; i < count; i++)
            {
                Temp.Add(GetListOfUserList(UserData[i]));
            }
            return Temp;
        }
        public int? DeleteUser(string name)
        {
            if(UserData.Where(s=> s.Name == name).Any())
            {
                UserData.RemoveAll(c=>c.Name == name);
                return 1;
            }
            return null;
        }
        public int? EditUser(string name,UserModel user)
        {
            if (UserData.Where(s => s.Name == name).Any())
            {
               var OldUser = UserData.FirstOrDefault(c => c.Name == name);
                if((user.Playlist.Count)>=1)
                {
                    foreach(var item in user.Playlist)
                    {
                        if(PlaylistData.Where(a=>a.PlaylistName == item).Any())
                        {
                            OldUser.Playlist.Add(item);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                user.Playlist = OldUser.Playlist;
                UserData.Remove(OldUser);
                UserData.Add(user);
                return 1;
            }
            return null;
        }
//        int count = playlist.SongsName.Count;
//                if (count>=1)
//                {
//                    foreach (var item in playlist.SongsName) { 
//                        if(SongData.Where(c => c.SongName == item).Any())
//                        {
//                            Oldlist.SongsName.Add(item);
//                        }
//                        else
//                        {
//                            return null;
//                        }
//                    }
//                }
//                playlist.SongsName = Oldlist.SongsName;
//PlaylistData.Remove(Oldlist);
//PlaylistData.Add(playlist);
        
    }
}
