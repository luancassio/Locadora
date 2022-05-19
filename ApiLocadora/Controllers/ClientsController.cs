using ApiLocadora.Application.Contracts;
using ApiLocadora.Domain;
using ApiLocadora.Application.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLocadora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;    
        public ClientsController(IClientService clientService)
        {    
            _clientService = clientService;   
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                if (clients == null) return NotFound("Nenhum registro encontrado.");

                return Ok(clients);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cliente. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]       
        public async Task<IActionResult> GetClientById(Guid id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null) return NotFound("Nenhum registro encontrado com o Id informado.");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cliente. Erro: {ex.Message}");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetClientByName(string name)
        {
            try
            {
                var client = await _clientService.GetClientByNameAsync(name);
                if (client == null) return NotFound("Nenhum registro encontrado com o nome informado.");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Cliente. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]     
        public async Task<IActionResult> PutClient(Guid id, ClientDto model)
        {
            try
            {
                var client = await _clientService.UpdateClient(id, model);
                if (client == null) return NotFound("Nenhum registro encontrado para atualização com o Id informado.");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar Cliente. Erro: {ex.Message}");
            }
        }

        [HttpPost]      
        public async Task<IActionResult> PostClient(ClientDto model)
        {
            try
            {
                var client = await _clientService.AddClients(model);
                if (client == null) return NoContent();

                if (client.Document == null) return NotFound("Já existe cliente cadastrado com o documento informado!");

                return Ok(client);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar Cliente. Erro: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null) return NotFound("Nenhum registro encontrado para delete com o Id informado.");

                await _clientService.DeleteClient(id);

                return Ok(client);
               
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Cliente. Erro: {ex.Message}");
            }
        }
    }
}
