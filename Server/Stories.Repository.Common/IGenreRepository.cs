using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Model;

namespace Stories.Repository.Common
{
    public interface IGenreRepository
    {
        Task<List<GenreModel>> GetGenresAsync();
        Task<List<GenreModel>> GetUsersGenresAsync(string UserId);
        Task<List<GenreModel>> GetOtherGenresAsync(string UserId);
        Task<List<GenreModel>> GetStoryGenresAsync(Guid StoryId);
        Task PostUserGenresAsync(string UserId, List<GenreModel> genreModels);
    }
}
