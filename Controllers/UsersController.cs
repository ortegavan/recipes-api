using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly JsonDataService<User> ds;

    public UsersController()
    {
        this.ds = new JsonDataService<User>("Data/Users.json");
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        return Ok(ds.GetById(id));
    }

    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] User user)
    {
        ds.Save(user);
        return NoContent();
    }
}
