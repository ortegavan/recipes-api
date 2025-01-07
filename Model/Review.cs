public class Review
{
    public required string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UserName { get; set; }
    public string? UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public string? RecipeId { get; set; }

}