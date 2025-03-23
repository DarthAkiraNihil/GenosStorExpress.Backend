using System.Text.Json.Serialization;

namespace GenosStorExpress.Application.Wrappers.Dashboard;

public class DashboardOrderWrapper {
    public long Id { get; set; }
    public string Orderer { get; set; }
    
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }
    public double Total { get; set; }
    
    [JsonPropertyName("items_count")]
    public int ItemsCount { get; set; }

    public DashboardOrderWrapper() {
        Orderer = String.Empty;
        CreatedAt = String.Empty;
    }
}