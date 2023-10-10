using NBPExchangeRates.Interfaces;
using NBPExchangeRates.ViewModel;

namespace NBPExchangeRates.Services
{
    public class DataProviderService : IDataProviderService
    {
        private readonly IDatabaseCommunicationService _databaseCommunicationService;
        private readonly INBPAPICommunicationService _nBPAPICommunicationService;

        public DataProviderService(IDatabaseCommunicationService databaseCommunicationService,
            INBPAPICommunicationService nBPAPICommunicationService)
        {
            _databaseCommunicationService = databaseCommunicationService;
            _nBPAPICommunicationService = nBPAPICommunicationService;
        }
        public async Task<NBPAPIResponseViewModel> GetExchangeRates()
        {
            try
            {
                //checks if exchange rates in database are up to date
                if (_databaseCommunicationService.IsExchangeRateInDatabaseUpToDate(out var lastPublicationsEffectiveDate))
                {
                    //if exchange rates in database are up to date, returns data from database 
                    return _databaseCommunicationService.GetExchangeRateFromDatabase();
                }
                //gets latest exchange rates from NBP API 
                var latestExchangeRates = await _nBPAPICommunicationService.GetLatestExchangeRatesFromApi();
                //if returned data is different than data stored in database, update exchange rates in database
                if (latestExchangeRates.Count > 0 && (!lastPublicationsEffectiveDate.HasValue || lastPublicationsEffectiveDate.Value != latestExchangeRates.First().EffectiveDate))
                {
                    await _databaseCommunicationService.SaveExchangeRateInDatabase(latestExchangeRates.First());
                }
                return latestExchangeRates.First();
            }
            catch
            {
                return new NBPAPIResponseViewModel();
            }
        }
    }
}
