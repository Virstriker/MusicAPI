using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Models;
using MusicAPI.Repos;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepo _repo;
        //private readonly ISongRepo _songRepo;
        public ArtistController(IArtistRepo repo)
        {
            _repo = repo;
            //_songRepo = songRepo;
        }
        [HttpPost]
        public IActionResult AddArtistRepo(ArtistModel artist)
        {
            _repo.AddArtist(artist);
            return Ok("Addeed Successfully");

        }
        [HttpPost("bulk")]
        public IActionResult AddArtistRepo1(List<ArtistModel> artist)
        {
            Parallel.ForEach(artist , artist1 => _repo.AddArtist(artist1));
            //_repo.AddArtist(artist);
            return Ok("Addeed Successfully");

        }
        [HttpGet]
        public IActionResult GetArtist()
        {
            //return _repo.GetArtistList();
            return Ok(_repo.GetArtistList());
        }
        [HttpDelete("{artist}")]
        public IActionResult DeleteArtist(string artist)
        {
            _repo.DeleteArtist(artist);
            _repo.DeleteArtistSongs(artist);
            return Ok("Deleted!");
        }
        [HttpPut("{artistName}")]
        public IActionResult UpdateArtist(string artistName,ArtistModel artist)
        {
            _repo.UpdateArtist(artistName, artist);
            _repo.ReplaceArtistOfSong(artistName, artist.ArtistName);
            return Ok("Updated");
        }
    }
}
