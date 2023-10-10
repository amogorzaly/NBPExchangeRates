using NBPExchangeRates.ViewModel;

namespace NBPExchangeRates.Interfaces
{
    public interface INBPAPICommunicationService
    {
        Task<List<NBPAPIResponseViewModel>> GetLatestExchangeRatesFromApi();
    }
}
