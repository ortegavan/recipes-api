using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly JsonDataService<Recipe> ds;

    public RecipesController()
    {
        this.ds = new JsonDataService<Recipe>("Data/Recipes.json");
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? userId, [FromQuery] string? search, [FromQuery] string? id)
    {
        var recipes = ds.GetAll();

        // Filtrar por IDs (múltiplos valores separados por vírgula)
        if (!string.IsNullOrEmpty(id))
        {
            var ids = id.Replace("[", "").Replace("]", "").Split(',').Select(i => i.Trim()).ToList();
            if (ids.Count > 0)
            {
                recipes = recipes.Where(r => ids.Contains(r.Id)).ToList();
            }
            else
            {
                recipes = [];
            }
        }

        // Filtrar por userId
        if (!string.IsNullOrEmpty(userId))
        {
            recipes = recipes.Where(r => r.UserId == userId).ToList();
        }

        // Filtrar por termo de busca (search)
        if (!string.IsNullOrEmpty(search))
        {
            recipes = recipes.Where(r =>
                (r.Name?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (r.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (r.Ingredients?.Any(i => i.Contains(search, StringComparison.OrdinalIgnoreCase)) ?? false) ||
                (r.Instructions?.Any(s => s.Contains(search, StringComparison.OrdinalIgnoreCase)) ?? false)
            ).ToList();
        }

        if (!recipes.Any())
        {
            recipes = [];
        }

        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        return Ok(ds.GetById(id));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Recipe recipe)
    {
        recipe.Id = Guid.NewGuid().ToString();
        ds.Save(recipe);
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] Recipe recipe)
    {
        ds.Save(recipe);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        ds.Delete(id);
        return NoContent();
    }
}