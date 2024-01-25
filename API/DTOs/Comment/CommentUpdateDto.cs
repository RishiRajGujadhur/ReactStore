// CommentUpdateDto.cs
using System.ComponentModel.DataAnnotations;

public class CommentUpdateDto
{
    [Required]
    public string Text { get; set; }
    // Add other properties that can be updated as needed
}