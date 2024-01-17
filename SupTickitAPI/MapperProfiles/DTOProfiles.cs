using AutoMapper;
using SupTickit.API.DTOs;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.MapperProfiles
{
    public class DTOProfiles : Profile
    {
        public DTOProfiles()
        {
            CreateMap<TicketCategoryInputDTO, TicketCategory>().ReverseMap();
            CreateMap<ProjectInputDTO, Project>().ReverseMap();
            CreateMap<ProjectWithCompaniesInputDTO, Project>();
            CreateMap<Project, ProjectGetAllDTO>().ReverseMap();
            CreateMap<CompanyCreateDTO, Company>();
            CreateMap<CompanyUpdateDTO, Company>();
            CreateMap<Ticket, TicketGetAllDTO>().ReverseMap();
            CreateMap<TicketCreateDTO, Ticket>();
            CreateMap<TicketUpdateDTO, Ticket>();
        }
    }
}
