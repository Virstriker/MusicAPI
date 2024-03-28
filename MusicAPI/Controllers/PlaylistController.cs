using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Models;
using MusicAPI.Repos;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepo _repo;
        public PlaylistController(IPlaylistRepo repo)
        {

            _repo = repo;
        }
        [HttpPost]
        public IActionResult AddPlayList(PlaylistModel playlist)
        {
            int? op = _repo.AddPlaylist(playlist);
            if(op != null)
            {
                return Ok("PlayList Added");
            }
            return BadRequest("Try again");
        }
        [HttpGet]
        public IActionResult GetPlayList()
        {
            return Ok(_repo.GetPlaylist());
        }
        [HttpGet("{PlaylistName}")]
        public IActionResult GetPlaylist(string PlaylistName) {
            return Ok(_repo.GetPlayListById(PlaylistName));

        }
        [HttpDelete("{PlayListName}")]
        public IActionResult Delete(string PlayListName) {
            int? op = _repo.RemovePlayList(PlayListName);
            if(op != null)
            {
                return Ok("Deleted");
            }
            return BadRequest("Not Found");
        }
        [HttpPut("{PlayListName}")]
        public IActionResult ReplacePlayList(string PlayListName, PlaylistModel playlist) {
            int? op = _repo.EditPlaylist(PlayListName, playlist);
            if (op != null)
            {
                return Ok("Edited");
            }
            return BadRequest("Not Found(Either song or playlist)");
        }
    }
}
