using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.SampleScheduler.Extensions;

namespace ApolloBank.SampleScheduler.TimerSchedulers
{
    public class TimerCheckDatabase : CronJobExtensions
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TimerCheckDatabase(IScheduleConfig<TimerCheckDatabase> config, IServiceProvider serviceProvider, IServiceScopeFactory serviceScopeFactory)
            : base(config.CronExpression, config.TimeZoneInfo, serviceProvider)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task DoWork(IServiceScope scope, CancellationToken cancellationToken)
        {
            Serilog.Log.Information("Verifying Database for scheduled transactions for today...");

            // Obter a data de hoje
            DateTime today = DateTime.Today;

            try
            {
                // Criar um novo escopo para resolver serviços com escopo
                using (var serviceScope = _serviceScopeFactory.CreateScope())
                {
                    // Resolva o ITransactionRepository dentro do novo escopo
                    var transactionSRepository = serviceScope.ServiceProvider.GetRequiredService<ITransactionsRepository>();

                    // Chame o método do repositório para obter as transações agendadas para hoje
                    List<Transaction> scheduledTransactions = await transactionSRepository.GetScheduledTransaction();

                    // Adicione sua lógica aqui para lidar com as transações obtidas
                    foreach (var transaction in scheduledTransactions)
                    {
                        var addScheduledRransactions = await transactionSRepository.CompleteScheduledTransaction(transaction.Id);
                    }

                    Serilog.Log.Information($"Found {scheduledTransactions.Count} scheduled transactions for today.");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error($"An error occurred while processing scheduled transactions: {ex.Message}");
            }
        }
    }
}

