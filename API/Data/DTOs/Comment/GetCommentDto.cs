public class GetCommentDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProductId { get; set; }
    public string Username { get; set; }
}
