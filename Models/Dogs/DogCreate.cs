

namespace Models.Dogs;

public class DogCreate

{
    public int OwnerId {get; set; }

    public string? Name { get; set; }
    public string? Breed { get; set; }

}