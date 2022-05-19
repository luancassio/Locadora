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
    public class ClientService : IClientService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IClientPersist _clientPersist;
        private readonly IMapper _mapper;

        public ClientService(IGeralPersist geralPersist,
                             IClientPersist clientPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _clientPersist = clientPersist;
            _mapper = mapper;
        }

        public async Task<ClientDto> AddClients(ClientDto model)
        {
            try
            {
                
                if (model == null) return null;

                var clientCurrent = _mapper.Map<Client>(model);

                var clients = await _clientPersist.GetAllClientsAsync();

                foreach (var client in clients) {
                    if (client.Document == clientCurrent.Document) {
                        clientCurrent.Document = null;

                        var clientCurrentResult = _mapper.Map<ClientDto>(client);
                        return clientCurrentResult;
                    }
                }


                clientCurrent.Address.ClientId = clientCurrent.Id;
                clientCurrent.UpdatedAt = DateTime.Now.ToLocalTime();              

                _geralPersist.Add<Client>(clientCurrent);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var clientReturn = await _clientPersist.GetClientByIdAsync(clientCurrent.Id);

                    return _mapper.Map<ClientDto>(clientReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteClient(Guid clientId)
        {
            try
            {
                var client = await _clientPersist.GetClientByIdAsync(clientId);
                if (client == null) throw new Exception("Cliente para delete não encontrado.");

                client.IsActive = false;
                client.Address.IsActive = false;               

                _geralPersist.Update(client);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ClientDto>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientPersist.GetAllClientsAsync();
                if (clients.Count == 0) return null;

                var result = _mapper.Map<List<ClientDto>>(clients);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientDto> GetClientByNameAsync(string name)
        {
            try
            {
                var client = await _clientPersist.GetClientByNameAsync(name);
                if (client == null) return null;

                var result = _mapper.Map<ClientDto>(client);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientDto> GetClientByIdAsync(Guid clientId)
        {
            try
            {
                var client = await _clientPersist.GetClientByIdAsync(clientId);
                if (client == null) return null;

                var result = _mapper.Map<ClientDto>(client);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientDto> UpdateClient(Guid clientId, ClientDto model)
        {
            try
            {
                var client = await _clientPersist.GetClientByIdAsync(clientId);
                if (client == null) return null;

                _mapper.Map(model, client);

                client.UpdatedAt = DateTime.Now.ToLocalTime();

                _geralPersist.Update<Client>(client);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filmReturn = await _clientPersist.GetClientByIdAsync(client.Id);

                    return _mapper.Map<ClientDto>(filmReturn);
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
