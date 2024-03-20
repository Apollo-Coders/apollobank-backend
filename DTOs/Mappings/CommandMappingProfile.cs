using ApolloBank.Models;
using AutoMapper;

namespace ApolloBank.DTOs.Mappings
{
    public class CommandMappingProfile : Profile
    {
       public CommandMappingProfile()
        public CommandMappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
                .ForMember(
                    dest => dest.Complement,
                    opt => opt.MapFrom(src => src.Address.Complement)
                )
                .ForMember(
                    dest => dest.Neighborhood,
                    opt => opt.MapFrom(src => src.Address.Neighborhood)
                )
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ReverseMap();
            ;
            CreateMap<User, UserDetailsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
                .ForMember(
                    dest => dest.Complement,
                    opt => opt.MapFrom(src => src.Address.Complement)
                )
                .ForMember(
                    dest => dest.Neighborhood,
                    opt => opt.MapFrom(src => src.Address.Neighborhood)
                )
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ReverseMap();
            CreateMap<User, UpdateUserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
                .ForMember(
                    dest => dest.Complement,
                    opt => opt.MapFrom(src => src.Address.Complement)
                )
                .ForMember(
                    dest => dest.Neighborhood,
                    opt => opt.MapFrom(src => src.Address.Neighborhood)
                )
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
