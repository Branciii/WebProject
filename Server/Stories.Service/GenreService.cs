using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Service.Common;
using Stories.Repository.Common;
using Stories.Model;

namespace Stories.Service
{
    class GenreService : IGenreService
    {
        protected IGenreRepository GenreRepository { get; private set; }


        public GenreService(IGenreRepository genreRepository)
        {
            this.GenreRepository = genreRepository;
        }

        public async Task<List<GenreModel>> GetGenresAsync()
        {
            return await GenreRepository.GetGenresAsync();
        }

        public async Task<List<GenreModel>> GetUsersGenresAsync(string UserId)
        {
            return await GenreRepository.GetUsersGenresAsync(UserId);
        }

        public async Task<bool> PostUsersGenreAsync(string UserId, string GenreName)
        {
            return await GenreRepository.PostUsersGenreAsync(UserId, GenreName);
        }
    }
}
