namespace MovieApp.DataAccess.Data.Entities;

public class Artist : EntityBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public override string ToString() => $"{base.ToString()} \tName: {FirstName} {LastName}";
}
