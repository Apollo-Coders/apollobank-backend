using ApolloBank.Models;
using AutoMapper;


namespace ApolloBank.DTOs.Mappings
{
    public class CommandMappingProfile : Profile
    {
        CommandMappingProfile()
        {
            CreateMap<Transaction, TransactionDTO>().ReverseMap();

            CreateMap<CreateCreditCardDTO, CreditCard>();
            CreateMap<CreditCard, CreditCardDetailsDTO>(); 

        }
    }
}
