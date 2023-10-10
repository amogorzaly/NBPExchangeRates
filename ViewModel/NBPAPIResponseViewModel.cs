using System.Text.Json.Serialization;

namespace NBPExchangeRates.ViewModel
{
    public class NBPAPIResponseViewModel
    {
        [JsonPropertyName("no")]
        public string Number { get; set; } = String.Empty;

        [JsonPropertyName("effectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonPropertyName("rates")]
        public List<ExchangeRateViewModel> Rates { get; set; } = new();
    }
}
