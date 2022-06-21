using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1Retake.Services;

namespace Test1Retake.Controllers
{

    [ApiController]
    [Route("api/albums")]
    public class AlbumsController : ControllerBase
    {
        public IDbService _dbService;

        public AlbumsController(IDbService dbS)
        {
            _dbService = dbS;
        }

        [HttpGet("{idAlbum}")]
        public ActionResult GetAlbumInfo([FromRoute] int idAlbum)
        {
            if (!_dbService.DoesGivenAlbumExist(idAlbum))
            {
                return NotFound("Album does not exist");
            }

            if (!_dbService.DoesAlbumHaveSongs(idAlbum))
            {
                return Ok(200);
            }

            var result = _dbService.GetAlbumWithSongs(idAlbum);

            return Ok(result);
        }

    }
}
