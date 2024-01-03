namespace MovieApp.AplicationServices.Components.FileCreator.Models;

public class Artist
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public override string ToString() => $"Name: {FirstName} {LastName}";
}
