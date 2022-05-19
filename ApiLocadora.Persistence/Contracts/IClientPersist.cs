using ApiLocadora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence.Contracts
{
    public interface IClientPersist
    {
        Task<Client> GetClientByNameAsync(string name);
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid clientId);
    }
}
