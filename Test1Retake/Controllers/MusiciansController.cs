using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1Retake.Services;

namespace Test1Retake.Controllers
{
    [ApiController]
    [Route("api/musicians")]
    public class MusiciansController : ControllerBase
    {
        public IDbService _dbService;

        public MusiciansController (IDbService dbS)
        {
            _dbService = dbS;
        }

        [HttpDelete("{idMusician}")]
        public ActionResult DeleteMusician ([FromRoute] int idMusician)
        {
            if (!_dbService.DoesMusicianExist(idMusician))
            {
                return NotFound(404);
            }

            if (!_dbService.IsMusicianInvolved(idMusician))
            {
                return BadRequest("Musician is not involved in any songs. Please try again.");
            }

            var result = _dbService.DeleteMusician(idMusician);
            return Ok(result);
        }

    }
}
