

using System.ComponentModel.DataAnnotations;

namespace Models.Posts;

public class PostCreate
{
    [Required]
    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int AuthorId { get; set; }

}