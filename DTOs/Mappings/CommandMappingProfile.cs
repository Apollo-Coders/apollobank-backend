using ApolloBank.Models;
using AutoMapper;


namespace ApolloBank.DTOs.Mappings
{
    public class CommandMappingProfile : Profile
    {
       public CommandMappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
        }
    }
}
