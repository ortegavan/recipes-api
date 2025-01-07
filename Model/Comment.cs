public class Comment
{
    public required string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UserName { get; set; }
    public string? UserId { get; set; }
    public int Rating { get; set; }
    public string? Text { get; set; }
    public string? RecipeId { get; set; }

}