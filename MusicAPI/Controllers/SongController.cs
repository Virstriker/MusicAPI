using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Models;
using MusicAPI.Repos;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepo _songRepo;
        //private readonly IArtistRepo _artistRepo;
        public SongController(ISongRepo songRepo)
        {
            _songRepo = songRepo;
            //_artistRepo = artistRepo;

        }
        [HttpPost]
        public IActionResult AddSong(SongModel song) { 
            int? op = _songRepo.AddSong(song);
            if (op != null)
            {
                return (Ok("Song Added"));
            }
            return BadRequest("Song exixt or Artist not found");
        }
        [HttpGet]
        public IActionResult GetAllSong() { 
            return Ok(_songRepo.GetSongs());
        }
        [HttpDelete("{Name}")]
        public IActionResult DeleteSong(string Name)
        {
            int? op = _songRepo.DeleteSong(Name);
            if (op != null)
            {
                return (Ok("Deleted"));
            }
            //_songRepo.RemoveSongFromList(Name);
            return (Ok("Not Found"));
        }
        [HttpPut("{songName}")]
        public IActionResult PutSong(string songName,SongModel song)
        {
            int? op = _songRepo.EditSong(songName, song);
            if(op != null) {
                return (Ok("Name Edited"));
            }
            return BadRequest("Song not found");
        }
    }
}
