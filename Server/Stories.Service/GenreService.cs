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

        public async Task<List<GenreModel>> GetOtherGenresAsync(string UserId)
        {
            return await GenreRepository.GetOtherGenresAsync(UserId);
        }

        public async Task<List<GenreModel>> GetStoryGenresAsync(Guid StoryId)
        {
            return await GenreRepository.GetStoryGenresAsync(StoryId);
        }

        public async Task PostUserGenresAsync(string UserId, List<GenreModel> genreModels)
        {
            await GenreRepository.PostUserGenresAsync(UserId, genreModels);
        }
    }
}
