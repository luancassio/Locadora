using ApiLocadora.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Application.Contracts
{
    public interface IFilmService
    {
        Task<FilmDto> AddFilms(FilmDto model);
        Task<FilmDto> UpdateFilm(Guid filmId, FilmDto model);
        Task<bool> DeleteFilm(Guid filmId, bool isActive);


        Task<List<FilmDto>> GetAllFilmsAsync();
        Task<FilmDto> GetFilmByTitleAsync(string title);
        Task<FilmDto> GetFilmByIdAsync(Guid filmId);

    }
}
