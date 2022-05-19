using System;
using System.Linq;
using ApiLocadora.Domain;
using ApiLocadora.Application.Dto;
using AutoMapper;

namespace ApiLocadora.Application.Helpers
{
    public class ApiLocadoraProfile : Profile
    {
        public ApiLocadoraProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Film, FilmDto>().ReverseMap();
            CreateMap<RentalCompany, RentalCompanyDto>().ReverseMap();            
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
