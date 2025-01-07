using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class LoginController : ControllerBase
{
    private readonly JsonDataService<Login> ds;

    public LoginController()
    {
        this.ds = new JsonDataService<Login>("Data/Login.json");
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(ds.GetAll());
    }
}