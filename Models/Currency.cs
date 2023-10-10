namespace NBPExchangeRates.Models
{
    public class Currency : DbRecordCommonFields
    {
        public int CurrencyId { get; set; }
        public required string Name { get; set; } 
        public required string Code { get; set; }
    }
}
