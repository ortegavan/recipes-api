public class Recipe
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IList<string> CategoryIds { get; set; }
    public string? ImagePath { get; set; }
    public IList<string> Ingredients { get; set; }
    public IList<string> Instructions { get; set; }
    public bool New { get; set; }
    public string? UserId { get; set; }

    public Recipe()
    {
        CategoryIds = [];
        Ingredients = [];
        Instructions = [];
    }
}