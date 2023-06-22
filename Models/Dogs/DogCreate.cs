//Models contain only information required to complete task

using System.ComponentModel.DataAnnotations;

namespace Models.Dogs;

public class DogCreate
{
    //including model validation via Attributes because we are retrieving information from the user
    [Required]
    public int OwnerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Breed { get; set; }

    [Required]
    public int ReqDistance { get; set; }  //since Not Null in table needed to include reqDistance and WalkingTime

    [Required]
    public int WalkingTime { get; set; }
}