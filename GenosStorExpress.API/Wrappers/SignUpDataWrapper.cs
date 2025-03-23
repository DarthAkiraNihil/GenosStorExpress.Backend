using System.Text.Json.Serialization;

namespace GenosStorExpress.API.Wrappers;

public class SignUpDataWrapper {
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonPropertyName("user_type")]
    public string UserType { get; set; }
    [JsonPropertyName("additional_data")]
    public IDictionary<string, dynamic> AdditionalData { get; set; }

    public SignUpDataWrapper() {
        Email = string.Empty;
        Password = string.Empty;
        UserType = string.Empty;
        AdditionalData = new Dictionary<string, dynamic>();
    }
}