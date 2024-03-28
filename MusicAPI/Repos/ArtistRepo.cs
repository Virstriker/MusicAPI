using MusicAPI.Models;

namespace MusicAPI.Repos
{
    public class ArtistRepo :DataBase, IArtistRepo
    {
        //private ISongRepo _songRepo;
        
        public ArtistRepo(/*ISongRepo songRepo*/) {
            //_songRepo = songRepo;
        }
        public int AddArtist(ArtistModel artist)
        {
            if (!ArtistList.Where(s => s.ArtistName == artist.ArtistName).Any())
            {
                ArtistList.Add(artist);
                return 1;
            }
                return 0;
        }
        public List<ArtistModel> GetArtistList() { 
            return ArtistList;
        }
        public void DeleteArtist(string artist)
        {
            ArtistList.RemoveAll(s=> s.ArtistName == artist);
            //_songRepo.DeleteArtistSongs(artist);
        }
        
        
        public int? UpdateArtist(string artistnName,ArtistModel artist)
        {
            if(ArtistList.Where(ar=>ar.ArtistName == artistnName).Any())
            {
                var PreArtist = ArtistList.FirstOrDefault(s=>s.ArtistName == artistnName);
                artist.ArtistSong = PreArtist.ArtistSong;
                ArtistList.Add(artist);
                ArtistList.RemoveAll(c => c.ArtistName == artistnName);
                return 1;
            }
            return null;
        }
        public int? DeleteArtistSongs(string ArtistName)
        {
            int operation = 0;
            if (SongData.Where(s => s.SongArtistName == ArtistName).Any())
            {
                SongData.RemoveAll(s => s.SongArtistName == ArtistName);
                operation = 1;
            }
            return operation;
        }
        public int? ReplaceArtistOfSong(string oldArtist, string newArtist)
        {
            if (SongData.Where(s => s.SongArtistName == oldArtist).Any())
            {
                foreach (SongModel song in SongData)
                {
                    if (song.SongArtistName == oldArtist)
                    {
                        song.SongArtistName = newArtist;
                    }
                }
                return 1;
            }
            return null;
        }
        
    }
}
