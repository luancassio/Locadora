using ApiLocadora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence.Contracts
{
    public interface IFilmPersist
    {
        Task<Film> GetFilmByTitleAsync(string title);
        Task<List<Film>> GetAllFilmsAsync();
        Task<Film> GetFilmByIdAsync(Guid filmId);
        Task<Film> DeleteFilmByIdAsync(Guid filmId);
    }
}
