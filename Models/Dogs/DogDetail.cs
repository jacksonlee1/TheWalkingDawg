
namespace Models.Dogs;

public class DogDetail

{
    public int Id{get;set;}
    public string Username{get;set;}
    public int OwnerId { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
}