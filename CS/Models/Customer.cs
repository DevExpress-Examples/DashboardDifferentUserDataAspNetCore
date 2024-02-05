using Newtonsoft.Json;

namespace AspNetCoreDashboard {
    public class Customer {
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }
        [JsonProperty("contactName")]
        public string ContactName { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
