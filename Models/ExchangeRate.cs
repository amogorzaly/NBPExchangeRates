using Microsoft.EntityFrameworkCore;

namespace NBPExchangeRates.Models
{
    public class ExchangeRate : DbRecordCommonFields
    {
        public int ExchangeRateId { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }
        public int EffectiveId { get; set; }
        public virtual Effective? Effective { get; set; }

        [Precision(18,6)]
        public decimal AverageCurrencyRate { get; set; }
    }
}
