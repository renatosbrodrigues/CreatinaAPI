namespace CreatinaAPI.CustomRequests;

public class CreateUserRequest
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }

}

public class EditUserRequest
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    
}
