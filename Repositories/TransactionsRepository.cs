using ApolloBank.Data;
using ApolloBank.Enums;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ApolloBank.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransactionsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<Transaction> AddTransaction(Transaction Transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions(int? id)
        {
            return await _appDbContext.Transactions.Where(x => x.Account_Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetCurrentMonthTransactions(int? id)
        {
            // Obtém a data atual
            DateTime currentDate = DateTime.Now;

            // Obtém o primeiro dia do mês atual
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Obtém o primeiro dia do próximo mês
            DateTime firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);

            // Filtra as transações para o mês atual para a conta especificada
            return await _appDbContext.Transactions
                .Where(x => x.Account_Id == id && x.Date >= firstDayOfMonth && x.Date < firstDayOfNextMonth)
                .ToListAsync();
        }


        public async Task<IEnumerable<Transaction>> GetLastSixMonthsTransactions(int? id)
        {
            // Obtém a data atual
            DateTime currentDate = DateTime.Now;

            // Calcula o primeiro dia do mês atual
            DateTime firstDayOfCurrentMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Calcula o primeiro dia do mês seis meses atrás
            DateTime firstDayOfSixMonthsAgo = firstDayOfCurrentMonth.AddMonths(-6);

            // Filtra as transações para os últimos seis meses para a conta especificada
            return await _appDbContext.Transactions
                .Where(x => x.Account_Id == id && x.Date >= firstDayOfSixMonthsAgo && x.Date < firstDayOfCurrentMonth)
                .ToListAsync();
        }


     



    }
}
