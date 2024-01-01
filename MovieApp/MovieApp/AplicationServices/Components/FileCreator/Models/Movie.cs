using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.AplicationServices.Components.FileCreator.Models;

public class Movie
{
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }

    public override string ToString() => $"{Title,-53} ({Year}) - {Universe,-20} - BoxOffice: {BoxOffice.ToString("C", new System.Globalization.CultureInfo("en-US")),20}";
}
