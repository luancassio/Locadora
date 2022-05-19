using ApiLocadora.Application.Dto;
using ApiLocadora.Domain;
using ApiLocadora.Persistence.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Application.Contracts
{
    public class FilmService : IFilmService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IFilmPersist _filmPersist;
        private readonly IMapper _mapper;

        public FilmService(IGeralPersist geralPersist,
                             IFilmPersist filmPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _filmPersist = filmPersist;
            _mapper = mapper;
        }

        public async Task<FilmDto> AddFilms(FilmDto model)
        {
            try
            {             
                if (model == null) return null;

                var film = _mapper.Map<Film>(model);

                _geralPersist.Add<Film>(film);              

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filmReturn = await _filmPersist.GetFilmByIdAsync(film.Id);

                    return _mapper.Map<FilmDto>(filmReturn);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFilm(Guid filmId, bool isActive)
        {
            try
            {
                var film = await _filmPersist.DeleteFilmByIdAsync(filmId);
                if (film == null) throw new Exception("Filme para delete não encontrado.");

                film.IsActive = isActive;          

                _geralPersist.Update(film);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FilmDto>> GetAllFilmsAsync()
        {
            try
            {
                var films = await _filmPersist.GetAllFilmsAsync();
                if (films.Count == 0) return null;

                var result = _mapper.Map<List<FilmDto>>(films);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FilmDto> GetFilmByTitleAsync(string title)
        {
            try
            {
                var film = await _filmPersist.GetFilmByTitleAsync(title);
                if (film == null) return null;

                var result = _mapper.Map<FilmDto>(film);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FilmDto> GetFilmByIdAsync(Guid filmId)
        {
            try
            {
                var film = await _filmPersist.GetFilmByIdAsync(filmId);
                if (film == null) return null;

                var result = _mapper.Map<FilmDto>(film);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FilmDto> UpdateFilm(Guid filmId, FilmDto model)
        {
            try
            {
                var film = await _filmPersist.GetFilmByIdAsync(filmId);
                if (film == null) return null;

                _mapper.Map(model, film);  
                
                film.UpdatedAt = DateTime.Now.ToLocalTime();

                _geralPersist.Update<Film>(film);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filmReturn = await _filmPersist.GetFilmByIdAsync(film.Id);

                    return _mapper.Map<FilmDto>(filmReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
