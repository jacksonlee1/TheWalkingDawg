

namespace Models.Dogs;

public class DogCreate

{


    public string? Name { get; set; }
    public string? Breed { get; set; }
    public int ReqDistance{get;set;}

    public int WalkingTime{get;set;}

}