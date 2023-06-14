
namespace Models.Dogs;

public class DogUpdate

{
    //Same attributes as DogCreate model.  Difference is the added id

    public int Id {get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public int ReqDistance { get; set; }  

    public int WalkingTime { get; set; }

}