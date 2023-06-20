
namespace Models.Dogs;

public class DogDetail
{
<<<<<<< HEAD
    public int Id{get;set;}
    public string Username{get;set;}
    public int OwnerId { get; set; }
=======
    public int OwnerId { get; set; }
    public int Id { get; set; }
    public string? Username { get; set; }  //from Users table
>>>>>>> 26eb8c9e50809f1ff3b01dc28706618b76806a80
    public string? Name { get; set; }
    public string? Breed { get; set; }
}