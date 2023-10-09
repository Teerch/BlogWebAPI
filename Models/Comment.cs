namespace Web.Models;

public class Comments : Time
{
	public int Id { get; set; }
	public string Comment { get; set; }
	public int UserId { get; set; }
	public int PostId { get; set; }
	public virtual User? User { get; set; }
	public virtual Posts? Post { get; set; }
}