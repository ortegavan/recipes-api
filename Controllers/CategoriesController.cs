using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly JsonDataService<Category> ds;

    public CategoriesController()
    {
        this.ds = new JsonDataService<Category>("Data/Categories.json");
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(ds.GetAll());
    }
}