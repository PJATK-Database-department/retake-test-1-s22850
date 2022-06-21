using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test1Retake.Dto;

namespace Test1Retake.Services
{
    public interface IDbService
    {
        bool DoesGivenAlbumExist(int idAlbum);
        bool DoesAlbumHaveSongs(int idAlbum);

        GetAlbumResponse GetAlbumWithSongs(int idAlbum);

        bool DoesMusicianExist(int idMusician);

        bool IsMusicianInvolved(int idMusician);

        int DeleteMusician(int idMusician);



    }
}
