using ApolloBank.DTOs;
using ApolloBank.Models;
using AutoMapper;


namespace ApolloBank.Data.Mappings
{
    public class CommandMappingProfile : Profile
    {
        CommandMappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
        }
    }
}
