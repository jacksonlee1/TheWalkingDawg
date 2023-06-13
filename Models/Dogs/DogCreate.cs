

namespace Models.Dogs;

public class DogCreate

{


    public string? Name { get; set; }
    public string? Breed { get; set; }
    public int ReqDistance{get;set;}  //since Not Null in table needed to include reqDistance and WalkingTime

    public int WalkingTime{get;set;}

}