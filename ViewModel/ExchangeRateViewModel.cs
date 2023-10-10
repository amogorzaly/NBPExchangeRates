using System.Text.Json.Serialization;

namespace NBPExchangeRates.ViewModel
{
    public class ExchangeRateViewModel
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = String.Empty;

        [JsonPropertyName("code")]
        public string Code { get; set; } = String.Empty;

        [JsonPropertyName("mid")]
        public decimal Mid { get; set; }
    }
}
