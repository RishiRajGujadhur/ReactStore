// CommentDto.cs
using API.RequestHelpers;

public class CommentDto : PaginationParams
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; } 
}
