using ApiLocadora.Domain;
using ApiLocadora.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence
{
    public class RentalCompanyPersist : IRentalCompanyPersist
    {
        private readonly ApiLocadoraContext _context;
        public RentalCompanyPersist(ApiLocadoraContext context)
        {
            _context = context;

        }
        public async Task<List<RentalCompany>> GetAllRentalCompanysAsync()
        {
            var query = await _context.RentalCompanys
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.IsActive != false)
                .ToListAsync();

            return query;
        }

        public async Task<RentalCompany> GetRentalCompanyByTitleFilmAsync(string filmName)
        {
            var query = await _context.RentalCompanys
                .Include(f => f.FilmName == filmName)                                
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.IsActive != false)
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task<RentalCompany> GetRentalCompanyByClientNameAsync(string clientName)
        {
            var query = await _context.RentalCompanys
                .Include(f => f.ClientName == clientName)
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Where(f => f.IsActive != false)
                .FirstOrDefaultAsync();

            return query;
        }

        public async Task<RentalCompany> GetRentalCompanyByIdAsync(Guid rentalCompanyId)
        {
            var query = await _context.RentalCompanys
                 .AsNoTracking()
                 .OrderBy(f => f.Id)
                 .Where(f => f.Id == rentalCompanyId && f.IsActive != false)
                 .FirstOrDefaultAsync(f => f.Id == rentalCompanyId);

            return query;
        }
    }
}