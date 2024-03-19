﻿using ApolloBank.Models;

namespace ApolloBank.DTOs
{
    public class CreateCreditCardDTO
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
        public string Number { get; set; }
        public int Cvc { get; set; }
        public DateTime ExpirationTime { get; set; }
        public double CreditUsed { get; set; }
        public double CreditLimit { get; set; }
        public int AccountId { get; set; }
    }
}