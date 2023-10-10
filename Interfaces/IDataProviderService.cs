using NBPExchangeRates.ViewModel;

namespace NBPExchangeRates.Interfaces
{
    public interface IDataProviderService
    {
        Task<NBPAPIResponseViewModel> GetExchangeRates();
    }
}
