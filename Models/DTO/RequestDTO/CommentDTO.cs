namespace WebApi.Models.DTO.RequestDTO;

public class CommentRequestDTO
{
    public string Comment { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
}
