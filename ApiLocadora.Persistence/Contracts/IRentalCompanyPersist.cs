using ApiLocadora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLocadora.Persistence.Contracts
{
    public interface IRentalCompanyPersist
    {
        Task<RentalCompany> GetRentalCompanyByTitleFilmAsync(string filmName);
        Task<RentalCompany> GetRentalCompanyByClientNameAsync(string clientName);
        Task<List<RentalCompany>> GetAllRentalCompanysAsync();
        Task<RentalCompany> GetRentalCompanyByIdAsync(Guid rentalCompanyId);
    }
}
