using ApiLocadora.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Application.Contracts
{
    public interface IRentalCompanyService
    {
        Task<RentalCompanyDto> AddRentalCompanys(RentalCompanyDto model);
        Task<RentalCompanyDto> UpdateRentalCompany(Guid rentalCompanyId, RentalCompanyDto model);
        Task<bool> DeleteRentalCompany(Guid rentalCompanyId);

        Task<List<RentalCompanyDto>> GetAllRentalCompanysAsync();
        Task<RentalCompanyDto> GetRentalCompanyByTitleFilmAsync(string filmName);
        Task<RentalCompanyDto> GetRentalCompanyByClientNameAsync(string clientName);
        Task<RentalCompanyDto> GetRentalCompanyByIdAsync(Guid rentalCompanyId);
    }
}
