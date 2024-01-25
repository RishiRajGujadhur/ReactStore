// CommentDto.cs
public class CommentDto
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; } 
}
