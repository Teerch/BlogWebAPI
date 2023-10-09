namespace Web.Models;

public class User : Time
{
    public int Id { get; set; }
    public string First_name { get; set; }
    public string Last_name { get; set; }
    public string User_name { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] Passwordkey { get; set; }
    public string? OtherEmail { get; set; }
    public virtual ICollection<Posts>? Posts { get; set; }
    public virtual ICollection<Comments>? Comments { get; set; }
}