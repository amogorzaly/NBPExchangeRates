using NBPExchangeRates.ViewModel;

namespace NBPExchangeRates.Interfaces
{
    public interface IDatabaseCommunicationService
    {
        bool IsExchangeRateInDatabaseUpToDate(out DateTime? lastPublicationsEffectiveDate);
        Task SaveExchangeRateInDatabase(NBPAPIResponseViewModel latestExchangeRates);
        NBPAPIResponseViewModel? GetExchangeRateFromDatabase();
    }
}
