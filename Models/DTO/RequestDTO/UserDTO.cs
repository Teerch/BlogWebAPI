namespace WebApi.Models.DTO.RequestDTO;

public class UserRequestDTO
{
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string User_name { get; set; }
    public string Email { get; set; }
    public string? OtherEmail { get; set; } = null;
    public string Password { get; set; }
}