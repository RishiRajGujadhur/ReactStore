// CommentCreateDto.cs
using System.ComponentModel.DataAnnotations;

public class CommentCreateDto
{
    [Required]
    public string Text { get; set; }
    public int ProductId { get; set; }
    // Add other required properties as needed
}