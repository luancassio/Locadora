using ApiLocadora.Domain;
using ApiLocadora.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence
{
    public class ClientPersist : IClientPersist
    {
        private readonly ApiLocadoraContext _context;
        public ClientPersist(ApiLocadoraContext context)
        {
            _context = context;

        }
        public async Task<List<Client>> GetAllClientsAsync()
        {
            var query = await _context.Clients
                .Include(c => c.Address)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.IsActive != false)
                .ToListAsync();

            return query;
        }

        public async Task<Client> GetClientByNameAsync(string name)
        {
            var query = await _context.Clients               
                .Include(c => c.Address)
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.Name.ToLower().Contains(name.ToLower()) && c.IsActive != false)
                .FirstOrDefaultAsync(c => c.Name == name);

            return query;
        }

        public async Task<Client> GetClientByIdAsync(Guid clientId)
        {
            var query = await _context.Clients
                 .Include(c => c.Address)
                 .AsNoTracking()
                 .OrderBy(c => c.Id)
                 .Where(c => c.Id == clientId && c.IsActive != false)
                 .FirstOrDefaultAsync(c => c.Id == clientId);

            return query;
        }
    }
}