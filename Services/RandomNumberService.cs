
using ApolloBank.Services.Interfaces;

namespace ApolloBank.Services
{
    public class RandomNumberService: IRandomNumber
    {
         public int GenerateRandomNumber(int length)
        {
            var random = new Random();
            string number = "";
            for (int i = 0; i < length; i++)
            {
                number += random.Next(0, 10).ToString();
            }
            return int.Parse(number);
        }

        public string GenerateRandomNumberString(int length)
        {
            var random = new Random();
            string number = "";
            for (int i = 0; i < length; i++)
            {
                number += random.Next(0, 10).ToString();
            }
            return number;
        }
    }

    
}