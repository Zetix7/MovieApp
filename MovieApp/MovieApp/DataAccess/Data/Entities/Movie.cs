namespace MovieApp.DataAccess.Data.Entities;

public class Movie : EntityBase
{
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }

    public override string ToString() => $"{base.ToString()} \t{Title} ({Year})\n\tUniversum: {Universe}\n\tBoxOffice: {BoxOffice:c}";
}
