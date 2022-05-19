using ApiLocadora.Domain;
using ApiLocadora.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence
{
    public class FilmPersist : IFilmPersist
    {
        private readonly ApiLocadoraContext _context;
        public FilmPersist(ApiLocadoraContext context)
        {
            _context = context;

        }
        public async Task<List<Film>> GetAllFilmsAsync()
        {
            var query = await _context.Films                
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.IsActive != false)
                .ToListAsync();

            return query;
        }

        public async Task<Film> GetFilmByTitleAsync(string title)
        {
            var query = await _context.Films               
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.Title.ToLower().Contains(title.ToLower()) && f.IsActive != false)
                .FirstOrDefaultAsync(f => f.Title == title);

            return query;
        }

        public async Task<Film> GetFilmByIdAsync(Guid filmId)
        {
            var query = await _context.Films               
                 .AsNoTracking()
                 .OrderBy(f => f.Id)
                 .Where(f => f.Id == filmId && f.IsActive != false)
                 .FirstOrDefaultAsync(f => f.Id == filmId);

            return query;
        }


        public async Task<Film> DeleteFilmByIdAsync(Guid filmId) {
            var query = await _context.Films
                 .AsNoTracking()
                 .OrderBy(f => f.Id)
                 .Where(f => f.Id == filmId)
                 .FirstOrDefaultAsync(f => f.Id == filmId);

            return query;
        }
    }
}