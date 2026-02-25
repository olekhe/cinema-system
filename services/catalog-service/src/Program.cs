using Cinema.MoviewCatalog.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

// Read version from environment, default to "v1"
var appVersion = Environment.GetEnvironmentVariable("APP_VERSION") ?? "v1";

// In-memory data store
var movies = new List<Movie>
{
    new(1, "Inception", "Sci-Fi", 2010, appVersion),
    new(2, "The Dark Knight", "Action", 2008, appVersion),
    new(3, "Interstellar", "Sci-Fi", 2014, appVersion)
};

// CRUD endpoints
// GET all
app.MapGet("/movies", () => movies)
    .WithName("GetMovies")
    .WithOpenApi();

// GET by id
app.MapGet("/movies/{id}", (int id) => 
    movies.FirstOrDefault(m => m.Id == id) is Movie movie
        ? Results.Ok(movie)
        : Results.NotFound())
    .WithName("GetMovieById")
    .WithOpenApi();

// POST (create)
app.MapPost("/movies", (Movie movie) =>
{
    var newMovie = movie with { Id = movies.Max(m => m.Id) + 1 };
    movies.Add(newMovie);
    return Results.Created($"/movies/{newMovie.Id}", newMovie);
})
    .WithName("CreateMovie")
    .WithOpenApi();

// PUT (update)
app.MapPut("/movies/{id}", (int id, Movie updatedMovie) =>
{
    var index = movies.FindIndex(m => m.Id == id);
    if (index == -1) return Results.NotFound();
    
    movies[index] = updatedMovie with { Id = id };
    return Results.Ok(movies[index]);
})
    .WithName("UpdateMovie")
    .WithOpenApi();

// DELETE
app.MapDelete("/movies/{id}", (int id) =>
{
    var index = movies.FindIndex(m => m.Id == id);
    if (index == -1) return Results.NotFound();
    
    movies.RemoveAt(index);
    return Results.NoContent();
})
    .WithName("DeleteMovie")
    .WithOpenApi();

app.MapGet("/", () => "Hello World!");
app.MapHealthChecks("/health");

app.Run();