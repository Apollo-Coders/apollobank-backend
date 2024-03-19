namespace ApolloBank.SampleScheduler.Factories
{
    public class DefaultServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultServiceScopeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IServiceScope CreateScope()
        {
            return _serviceProvider.CreateScope();
        }
    }
}
