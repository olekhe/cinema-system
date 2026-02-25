namespace Cinema.MoviewCatalog.Models;

public record Movie(int Id, string Title, string Genre, int Year, string ApiVersion = "v1");
