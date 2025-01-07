using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly JsonDataService<Favorite> ds;

    public FavoritesController()
    {
        this.ds = new JsonDataService<Favorite>("Data/Favorites.json");
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? userId, [FromQuery] string? recipeId)
    {
        var favorites = ds.GetAll();

        // Filtrar por userId
        if (!string.IsNullOrEmpty(userId))
        {
            favorites = favorites.Where(f => f.UserId == userId).ToList();
        }

        // Filtrar por recipeId
        if (!string.IsNullOrEmpty(recipeId))
        {
            favorites = favorites.Where(f => f.RecipeId == recipeId && f.UserId == userId).ToList();
        }

        if (!favorites.Any())
        {
            favorites = [];
        }

        return Ok(favorites);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Favorite favorite)
    {
        ds.Save(favorite);
        return CreatedAtAction(nameof(Get), new { id = favorite.Id }, favorite);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        ds.Delete(id);
        return NoContent();
    }
}