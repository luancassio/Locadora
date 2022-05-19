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
    public class FilmsController : ControllerBase
    {
        private readonly IFilmService _filmService;
        
        public FilmsController(IFilmService filmService)
        {    
            _filmService = filmService; 
        }

        [HttpGet]       
        public async Task<IActionResult> Get()
        {
            try
            {
                var films = await _filmService.GetAllFilmsAsync();
                if (films == null) return NotFound("Nenhum registro encontrado.");

                return Ok(films);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar filme. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]       
        public async Task<IActionResult> GetFilmById(Guid id)
        {
            try
            {
                var film = await _filmService.GetFilmByIdAsync(id);
                if (film == null) return NotFound("Nenhum registro encontrado com o Id informado.");

                return Ok(film);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar filme. Erro: {ex.Message}");
            }
        }

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetFilmByTitle(string title)
        {
            try
            {
                var film = await _filmService.GetFilmByTitleAsync(title);
                if (film == null) return NotFound("Nenhum registro encontrado com o Título informado.");

                return Ok(film);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar filme. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]     
        public async Task<IActionResult> PutFilm(Guid id, FilmDto model)
        {
            try
            {
                var film = await _filmService.UpdateFilm(id, model);
                if (film == null) return NotFound("Nenhum registro encontrado para atualização com o Id informado.");

                return Ok(film);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar filme. Erro: {ex.Message}");
            }
        }

        [HttpPost]      
        public async Task<IActionResult> PostFilm(FilmDto model)
        {
            try
            {
                var film = await _filmService.AddFilms(model);
                if (film == null) return NoContent();

                return Ok(film);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar filme. Erro: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFilm(Guid id)
        {
            try
            {
                var film = await _filmService.GetFilmByIdAsync(id);
                if (film == null) return NotFound("Nenhum registro encontrado para delete com o Id informado.");

                await _filmService.DeleteFilm(id, false);                

                return Ok(film);
               
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar filme. Erro: {ex.Message}");
            }
        }
    }
}
