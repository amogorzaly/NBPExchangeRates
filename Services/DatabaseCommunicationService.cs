using System.Data;
using Microsoft.Extensions.Caching.Memory;
using NBPExchangeRates.Interfaces;
using NBPExchangeRates.Models;
using NBPExchangeRates.ViewModel;

namespace NBPExchangeRates.Services
{
    public class DatabaseCommunicationService : IDatabaseCommunicationService
    {
        private readonly NBPContext _context;
        private readonly IMemoryCache _memoryCache;
        public DatabaseCommunicationService(NBPContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public bool IsExchangeRateInDatabaseUpToDate(out DateTime? lastPublicationsEffectiveDate)
        {
            var lastPublication = _context.Effective.Where(effective => effective.IsActive).OrderByDescending(effective => effective.EffectiveDate).FirstOrDefault();
            if (lastPublication is { EffectiveDate: not null } && lastPublication.EffectiveDate.Value.Date == DateTime.Now.Date)
            {
                lastPublicationsEffectiveDate = null;
                return true;
            }
            lastPublicationsEffectiveDate = lastPublication?.EffectiveDate;
            return false;
        }
        public async Task SaveExchangeRateInDatabase(NBPAPIResponseViewModel latestExchangeRates)
        {
            var newEffective = new Effective()
            {
                EffectiveDate = latestExchangeRates.EffectiveDate,
                Number = latestExchangeRates.Number,
                IsActive = true,
                AddDate = DateTime.Now
            };
            _context.Effective.Add(newEffective);

            var currencies = GetCurrenciesDictionary();

            if (!currencies.Any())
            {
                return;
            }
            foreach (var rate in latestExchangeRates.Rates)
            {
                var newExchangeRate = new ExchangeRate
                {
                    IsActive = true,
                    AddDate = DateTime.Now,
                    AverageCurrencyRate = rate.Mid,
                    CurrencyId = currencies.First(x => x.Code.Equals(rate.Code)).CurrencyId,
                    EffectiveId = newEffective.EffectiveId,
                    Effective = newEffective
                };
                _context.ExchangeRate.Add(newExchangeRate);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
            }
        }
        private List<(int CurrencyId, string Code)> GetCurrenciesDictionary()
        {
            return _context.Currency.Where(currency => currency.IsActive).ToList()
                .Select(currency => (currency.CurrencyId, currency.Code)).ToList(); 
        }
        public NBPAPIResponseViewModel? GetExchangeRateFromDatabase()
        {
            if (_memoryCache.TryGetValue(ExchangeRateCacheKey, out NBPAPIResponseViewModel? cachedValue) && cachedValue is not null)
            {
                return cachedValue;
            }
            var result = (from effective in _context.Effective
                          where _context.ExchangeRate.Any(exRate => exRate.IsActive && exRate.EffectiveId == effective.EffectiveId)
                          orderby effective.EffectiveDate descending
                          select new NBPAPIResponseViewModel
                          {
                              EffectiveDate = effective.EffectiveDate.Value,
                              Number = effective.Number,
                              Rates = (
                                        from rate in _context.ExchangeRate
                                        where rate.IsActive && rate.EffectiveId == effective.EffectiveId
                                        select new ExchangeRateViewModel
                                        {
                                            Code = rate.Currency.Code,
                                            Currency = rate.Currency.Name,
                                            Mid = rate.AverageCurrencyRate
                                        }
                                    ).ToList()
                          }).FirstOrDefault();
            
            _memoryCache.Set(ExchangeRateCacheKey, result, TimeSpan.FromMinutes(10));
           
            return result;
        }

        private static readonly string ExchangeRateCacheKey = "latestExchangeRateFromDatabase";
    }
}
