using ApiLocadora.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Application.Contracts
{
    public interface IClientService
    {
        Task<ClientDto> AddClients(ClientDto model);
        Task<ClientDto> UpdateClient(Guid clientId, ClientDto model);
        Task<bool> DeleteClient(Guid clientId);

        Task<List<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByNameAsync(string name);
        Task<ClientDto> GetClientByIdAsync(Guid clientId);
    }
}
