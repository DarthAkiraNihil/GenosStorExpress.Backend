using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Dashboard {
    public class DashboardInfoWrapper {
        [JsonPropertyName("logged_admin")]
        public string LoggedAdmin { get; set; }
        
        [JsonPropertyName("registered_users")]
        public int RegisteredUsers { get; set; }
        
        [JsonPropertyName("registered_individual_entities")]
        public int RegisteredIndividualEntities { get; set; }
        
        [JsonPropertyName("registered_verified_legal_entities")]
        public int RegisteredLegalVerifiedEntities { get; set; }
        
        [JsonPropertyName("registered_waiting_for_verification_legal_entities")]
        public int LegalEntitiesWaitingForVerification { get; set; }
        
        [JsonPropertyName("active_orders_count")]
        public int ActiveOrdersCount { get; set; }
        
        [JsonPropertyName("last_order")]
        public DashboardOrderWrapper LastOrder { get; set; }

        public DashboardInfoWrapper() {
            LoggedAdmin = string.Empty;
            LastOrder = new DashboardOrderWrapper();
        }
    }

    
}