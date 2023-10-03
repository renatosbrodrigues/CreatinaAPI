namespace CreatinaAPI.Responses;

public class PublicUserResponse 
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
}

public class PrivateUserResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }

}
