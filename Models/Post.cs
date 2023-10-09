namespace Web.Models;

public class Posts : Time
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Post { get; set; }
    public int UserId { get; set; }
    public int Likes { get; set; }
    public virtual ICollection<Comments>? Comments { get; set; }
    public virtual User? User { get; set; }
}