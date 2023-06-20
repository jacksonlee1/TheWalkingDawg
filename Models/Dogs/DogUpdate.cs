
using System.ComponentModel.DataAnnotations;

namespace Models.Dogs;

public class DogUpdate
{
    //Same attributes as DogCreate model.  Difference is the added id
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Breed { get; set; }

    [Required]
    public int ReqDistance { get; set; }

    [Required]
    public int WalkingTime { get; set; }
}