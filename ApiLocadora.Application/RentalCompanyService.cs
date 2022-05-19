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
    public class RentalCompanyService : IRentalCompanyService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IRentalCompanyPersist _rentalCompanyPersist;
        private readonly IClientPersist _clientPersist;
        private readonly IFilmPersist _filmPersist;
        private readonly IMapper _mapper;

        public RentalCompanyService(IGeralPersist geralPersist,
                             IRentalCompanyPersist rentalCompanyPersist,
                             IClientPersist clientPersist,
                             IFilmPersist filmPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _rentalCompanyPersist = rentalCompanyPersist;
            _clientPersist = clientPersist;
            _filmPersist = filmPersist;
            _mapper = mapper;
        }

        public async Task<RentalCompanyDto> AddRentalCompanys(RentalCompanyDto model)
        {
            try
            {
                if (model == null) return null;

                var client = await _clientPersist.GetClientByNameAsync(model.ClientName);
                if (client == null)
                {
                    model.ClientName = null;
                    return model;
                }

                var film = await _filmPersist.GetFilmByTitleAsync(model.FilmName);
                if (film == null)
                {
                    model.FilmName = null;
                    return model;
                }
                
                var rentalCompany = _mapper.Map<RentalCompany>(model);

                rentalCompany.ClientId = client.Id;
                rentalCompany.FilmId = film.Id;

                _geralPersist.Add<RentalCompany>(rentalCompany);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var rentalCompanyReturn = await _rentalCompanyPersist.GetRentalCompanyByIdAsync(rentalCompany.Id);

                    return _mapper.Map<RentalCompanyDto>(rentalCompanyReturn);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteRentalCompany(Guid rentalCompanyId)
        {
            try
            {
                var rentalCompany = await _rentalCompanyPersist.GetRentalCompanyByIdAsync(rentalCompanyId);
                if (rentalCompany == null) throw new Exception("Locação para delete não encontrado.");

                rentalCompany.IsActive = false;          

                _geralPersist.Update(rentalCompany);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RentalCompanyDto>> GetAllRentalCompanysAsync()
        {           
            try
            {
                var rentalCompanys = await _rentalCompanyPersist.GetAllRentalCompanysAsync();
                if (rentalCompanys.Count == 0) return null;

                var result = _mapper.Map<List<RentalCompanyDto>>(rentalCompanys);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RentalCompanyDto> GetRentalCompanyByTitleFilmAsync(string filmName)
        {            
            try
            {
                var rentalCompany = await _rentalCompanyPersist.GetRentalCompanyByTitleFilmAsync(filmName);
                if (rentalCompany == null) return null;

                var result = _mapper.Map<RentalCompanyDto>(rentalCompany);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RentalCompanyDto> GetRentalCompanyByClientNameAsync(string clientName)
        {           
            try
            {
                var rentalCompany = await _rentalCompanyPersist.GetRentalCompanyByClientNameAsync(clientName);
                if (rentalCompany == null) return null;

                var result = _mapper.Map<RentalCompanyDto>(rentalCompany);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RentalCompanyDto> GetRentalCompanyByIdAsync(Guid rentalCompanyId)
        {
            try
            {
                var rentalCompany = await _rentalCompanyPersist.GetRentalCompanyByIdAsync(rentalCompanyId);
                if (rentalCompany == null) return null;

                var result = _mapper.Map<RentalCompanyDto>(rentalCompany);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RentalCompanyDto> UpdateRentalCompany(Guid rentalCompanyId, RentalCompanyDto model)
        {            
            try
            {
                var rentalCompany = await _rentalCompanyPersist.GetRentalCompanyByIdAsync(rentalCompanyId);
                if (rentalCompany == null) return null;

                _mapper.Map(model, rentalCompany);

                rentalCompany.UpdatedAt = DateTime.Now.ToLocalTime();

                _geralPersist.Update<RentalCompany>(rentalCompany);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var rentalCompanyReturn = await _rentalCompanyPersist.GetRentalCompanyByIdAsync(rentalCompany.Id);

                    return _mapper.Map<RentalCompanyDto>(rentalCompanyReturn);
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
