
namespace Models.Dogs;

public class DogDetail

{
    public int OwnerId { get; set; }
    public int Id{get;set;}
    public string? Username{get;set;}  //from Users table
    public string? Name { get; set; }
    public string? Breed { get; set; }

}