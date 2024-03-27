using AutoMapper;
using Suptickit.Domain.Models;
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
            CreateMap<PartCategoryCreateDTO, PartCategory>();
            CreateMap<PartCategoryEditDTO, PartCategory>(MemberList.Source);
            CreateMap<PartCreateDTO, Part>(MemberList.Source);
            CreateMap<PartUpdateDTO, Part>(MemberList.Source);
            CreateMap<Part, PartGetAllDTO>(MemberList.Source);
            CreateMap<Customer, CustomerGetAllDTO>(MemberList.Source);
            CreateMap<CustomerUpdateDTO, Customer>(MemberList.Source);
            CreateMap<CustomerCreateDTO, Customer>(MemberList.Source);
            CreateMap<Vehicle, VehicleGetAllDTO>(MemberList.Source);
            CreateMap<VehicleUpdateDTO, Vehicle>(MemberList.Source);
            CreateMap<VehicleCreateDTO, Vehicle>(MemberList.Source);
            CreateMap<TaxOrBonus, TaxOrBonusGetAllDTO>(MemberList.Source);
            CreateMap<TaxOrBonusUpdateDTO, TaxOrBonus>(MemberList.Source);
            CreateMap<TaxOrBonusCreateDTO, TaxOrBonus>(MemberList.Source);
            CreateMap<Quote, QuoteGetAllDTO>(MemberList.Source);
            CreateMap<QuoteUpdateDTO, Quote>();
            CreateMap<QuoteCreateDTO, Quote>(MemberList.Source);
            CreateMap<QuoteDetailCreateDTO, QuoteDetail>(MemberList.Source);
            CreateMap<QuoteDetailUpdateDTO, QuoteDetail>(MemberList.Source);
            CreateMap<TaxOrBonusApplied, TaxOrBonusAppliedGetDTO>(MemberList.Source);
            CreateMap<QuoteDetail, QuoteDetailGetAllDTO>(MemberList.Source);
        }
    }
}
