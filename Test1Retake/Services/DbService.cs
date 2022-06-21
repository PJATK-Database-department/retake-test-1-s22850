using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Test1Retake.Dto;

namespace Test1Retake.Services
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;

        public DbService (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }


        public int DeleteMusician(int idMusician)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var command = new SqlCommand("DELETE Musician WHERE idMusician = @idMusician", connection);
                command.Parameters.AddWithValue("@IdMusician", idMusician);

                connection.Open();

                var result = command.ExecuteScalar();


                return DoesMusicianExist(idMusician) ? 1 : 0;


            }
        }

        public bool DoesAlbumHaveSongs(int idAlbum)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT IIF(1) > 0,1,0 FROM Track WHERE IdMusicAlbum = @IdAlbum", connection);
                command.Parameters.AddWithValue("@IdAlbum", idAlbum);

                connection.Open();

                var result = command.ExecuteScalar();

                return Convert.ToBoolean(result);


            }
        }

        public bool DoesGivenAlbumExist(int idAlbum)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT IIF(1) > 0, 1, 0 FROM Album WHERE IdAlbum = @IdAlbum", connection);
                command.Parameters.AddWithValue("@IdAlbum", idAlbum);

                connection.Open();

                var result = command.ExecuteScalar();

                return Convert.ToBoolean(result);


            }
        }

        public bool DoesMusicianExist(int idMusician)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT IIF(1) > 0,1,0 FROM Musician WHERE IdMusician = @IdMusician", connection);
                command.Parameters.AddWithValue("@IdMusician", idMusician);

                connection.Open();

                var result = command.ExecuteScalar();

                return Convert.ToBoolean(result);


            }
        }

        public GetAlbumResponse GetAlbumWithSongs(int idAlbum)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Album, Track WHERE IdAlbum = @IdAlbum" +
                    "and IdMusicAlbum = @IdAlbum" +
                    "ORDER BY Duration ASC", connection);
                command.Parameters.AddWithValue("@IdAlbum", idAlbum);
            

                connection.Open();

                var reader = command.ExecuteReader();

                reader.Read();

                var result = reader.GetString(idAlbum).Split(",");
                

                return new GetAlbumResponse
                {
                    IdAlbum = Int32.Parse(result[0]),
                    AlbumName = result[1],
                    PublishDate = DateTime.Parse(result[2]),
                    IdMusicLabel = Int32.Parse(result[3]),
                    tracks = new List<Track>
                    {

                    }
                };

            }
        }

        public bool IsMusicianInvolved(int idMusician)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("" +
                    "SELECT IIF(1) > 0,1,0 " +
                    "FROM Musician_Track, Track " +
                    "WHERE IdMusician = @IdMusician and IdMusicAlbum is NULL", connection);
                command.Parameters.AddWithValue("@IdMusician", idMusician);

                connection.Open();

                var result = command.ExecuteScalar();

                return Convert.ToBoolean(result);


            }
        }
    }
}
