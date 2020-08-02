﻿using System;
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
        Task<bool> PostUsersGenreAsync(string UserId, string GenreName);
    }
}
