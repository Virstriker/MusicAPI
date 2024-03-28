using MusicAPI.Models;

namespace MusicAPI.Repos
{
    public class SongRepo : DataBase, ISongRepo
    {
        //private IArtistRepo _artistRepo;
        
        public SongRepo(/*IArtistRepo artistRepo*/) {
            //_artistRepo = artistRepo;
        }
        public int? AddSong(SongModel song)
        {
            if (!SongData.Where(s => s.SongName == song.SongName).Any())
            {
                //_artistRepo.AddArtistSong(song.SongArtistName,song.SongName);
                SongData.Add(song);
                var artist = SongData.FirstOrDefault(s => s.SongName == song.SongName);
                string name = artist.SongArtistName;
                foreach(ArtistModel artistModel in ArtistList)
                {
                    Console.WriteLine(artistModel.ArtistName);
                    if (artistModel.ArtistName == name)
                    {
                        artistModel.ArtistSong.Add(song.SongName);
                    }
                }
                return 1;
            }
            return null;
        }
        public List<SongModel> GetSongs()
        {
            return SongData;
        }
        public int? DeleteSong(string songName)
        {
            if (SongData.Where(c=>c.SongName == songName).Any())
            {
                SongData.RemoveAll(s => s.SongName == songName);
                //_artistRepo.RemoveSongFromList(songName);
            }
            if (ArtistList.Where(s => s.ArtistSong.Contains(songName)).Any())
            {
                foreach (ArtistModel artist in ArtistList)
                {
                    if (artist.ArtistSong.Contains(songName))
                    {
                        artist.ArtistSong.Remove(songName);
                    }
                }
                return 1;
            }
            return null;
            
        }
        public int? EditSong(string OldSong, SongModel NewSong)
        {
            if (SongData.Where(s => s.SongName == OldSong).Any())
            {
                var song = SongData.FirstOrDefault(s => s.SongName == OldSong);
                NewSong.SongDuration = song.SongDuration;
                NewSong.SongArtistName = song.SongArtistName;
                SongData.RemoveAll(c=>c.SongName == OldSong);
                song.SongName = NewSong.SongName;
                SongData.Add(song);
                foreach (ArtistModel artist in ArtistList)
                {
                    if(artist.ArtistSong.Contains(OldSong))
                    {
                        artist.ArtistSong.Remove(OldSong);
                        artist.ArtistSong.Add(NewSong.SongName);
                    }
                }
                var play = PlaylistData.FirstOrDefault(c=>c.SongsName.Contains(OldSong));
                if(play != null)
                {
                PlaylistData.Remove(play);
                play.SongsName.Remove(OldSong);
                play.SongsName.Add(NewSong.SongName);
                PlaylistData.Add(play);

                }
                return 1;
            }
            return null;
        }
    }
}
