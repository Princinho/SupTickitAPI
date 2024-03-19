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
            CreateMap<TicketCategoryEditDTO, TicketCategory>().ReverseMap();
            CreateMap<TicketCategory, TicketCategoryOutDTO>();
            CreateMap<MessageCreateDTO, Message>();
            CreateMap<Message, MessageGetAllDTO>();
            CreateMap<ProjectInputDTO, Project>().ReverseMap();
            CreateMap<ProjectEditDTO, Project>().ReverseMap();
            CreateMap<ProjectWithCompaniesInputDTO, Project>();
            CreateMap<Project, ProjectGetAllDTO>().ReverseMap();
            CreateMap<CompanyCreateDTO, Company>();
            CreateMap<CompanyUpdateDTO, Company>();
            CreateMap<Ticket, TicketGetAllDTO>().ReverseMap();
            CreateMap<TicketCreateDTO, Ticket>();
            CreateMap<TicketUpdateDTO, Ticket>();
            CreateMap<User, UserLoginOutDTO>();
            CreateMap<User, UsersGetAllDTO>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<UsersEditDTO, User>();
            CreateMap<UserWithRolesCreateDTO, User>();
            CreateMap<Company, CompanyGetAllDTO>();
            CreateMap<TicketLog, TicketLogDTO>();
        }
    }
}
