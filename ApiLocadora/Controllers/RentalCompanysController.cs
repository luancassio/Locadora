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
    public class RentalCompanysController : ControllerBase
    {
        private readonly IRentalCompanyService _rentalCompanyService;
        private readonly IFilmService _filmService;     

        public RentalCompanysController(IRentalCompanyService rentalCompanyService, 
                                        IFilmService filmService)
        {
            _rentalCompanyService = rentalCompanyService;
            _filmService = filmService;     
        }

        [HttpGet]       
        public async Task<IActionResult> Get()
        {
            try
            {
                var rentalCompanys = await _rentalCompanyService.GetAllRentalCompanysAsync();
                if (rentalCompanys == null) return NotFound("Nenhum registro encontrado.");

                return Ok(rentalCompanys);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar locação. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]       
        public async Task<IActionResult> GetRentalCompanyById(Guid id)
        {
            try
            {
                var rentalCompany = await _rentalCompanyService.GetRentalCompanyByIdAsync(id);
                if (rentalCompany == null) return NotFound("Nenhum registro encontrado com o Id informado.");

                return Ok(rentalCompany);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar locação. Erro: {ex.Message}");
            }
        }

        [HttpGet("titleFilm/{titleFilm}")]
        public async Task<IActionResult> GetRentalCompanyByTitleFilm(string titleFilm)
        {
            try
            {
                var rentalCompany = await _rentalCompanyService.GetRentalCompanyByTitleFilmAsync(titleFilm);
                if (rentalCompany == null) return NotFound("Nenhum registro encontrado com o Título informado.");

                return Ok(rentalCompany);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar locação. Erro: {ex.Message}");
            }
        }

        [HttpGet("clientName/{clientName}")]
        public async Task<IActionResult> GetRentalCompanyByClientName(string clientName)
        {
            try
            {
                var rentalCompany = await _rentalCompanyService.GetRentalCompanyByClientNameAsync(clientName);
                if (rentalCompany == null) return NotFound("Nenhum registro encontrado com o Cliente informado.");

                return Ok(rentalCompany);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar locação. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]     
        public async Task<IActionResult> PutRentalCompany(Guid id, RentalCompanyDto model)
        {
            try
            {
                var rentalCompany = await _rentalCompanyService.UpdateRentalCompany(id, model);

                if (rentalCompany == null) return NotFound("Nenhum registro encontrado para atualização com o Id informado.");

                return Ok(rentalCompany);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar locação. Erro: {ex.Message}");
            }
        }

        [HttpPost]      
        public async Task<IActionResult> PostRentalCompany(RentalCompanyDto model)
        {
            try
            {
                var rentalCompany = await _rentalCompanyService.AddRentalCompanys(model);

                if (rentalCompany == null) return NotFound("Existem algum dado inconsistente.");

                if (rentalCompany.ClientName == null) return NotFound("Não existem cliente cadastrado com nome informado.");

                if (rentalCompany.FilmName == null) return NotFound("Não existem filme cadastrado com título informado.");


                //await _filmService.DeleteFilm(rentalCompany.FilmId, false);

                return Ok(rentalCompany);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar locação. Erro: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRentalCompany(Guid id)
        {
            try
            {
                var rentalCompany = await _rentalCompanyService.GetRentalCompanyByIdAsync(id);
                if (rentalCompany == null) return NotFound("Nenhum registro encontrado para delete com o Id informado.");

                await _rentalCompanyService.DeleteRentalCompany(id);

                //await _filmService.DeleteFilm(rentalCompany.FilmId, true);


                return Ok(rentalCompany);
               
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar locação. Erro: {ex.Message}");
            }
        }
    }
}
