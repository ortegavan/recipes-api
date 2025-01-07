using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly JsonDataService<Comment> ds;

    public CommentsController()
    {
        this.ds = new JsonDataService<Comment>("Data/Comments.json");
    }

    [HttpGet]
    public IActionResult Get([FromQuery] string? recipeId)
    {
        if (!string.IsNullOrEmpty(recipeId))
        {
            var comments = ds.GetAll().Where(c => c.RecipeId == recipeId).ToList();
            return Ok(comments);
        }

        return Ok(ds.GetAll());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Comment comment)
    {
        ds.Save(comment);
        return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
    }
}