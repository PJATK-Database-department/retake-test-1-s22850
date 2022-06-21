using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1Retake.Dto
{
    public class Track
    {
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public float Duration { get; set; }
        public int IdMusicAlbum { get; set; }
    }

    public class GetAlbumResponse
    {

        public GetAlbumResponse()
        {

        }
        public int IdAlbum { get; set; }

        public string AlbumName { get; set; }

        public DateTime PublishDate { get; set; }

        public int IdMusicLabel { get; set; }

        public ICollection<Track> tracks { get; set; }

    }
}
