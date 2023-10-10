namespace NBPExchangeRates.Models
{
    public class Effective : DbRecordCommonFields
    {
        public int EffectiveId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string? Number { get; set; }
    }
}
