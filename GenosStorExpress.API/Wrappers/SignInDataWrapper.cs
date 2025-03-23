namespace GenosStorExpress.API.Wrappers;

public class SignInDataWrapper {
    public string Username { get; set; }
    public string Password { get; set; }
    
    public SignInDataWrapper() {
        Username = string.Empty;
        Password = string.Empty;
    }
}