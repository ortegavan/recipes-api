public class User
{
    public required string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime Birthday { get; set; }
    public string? Password { get; set; }

}