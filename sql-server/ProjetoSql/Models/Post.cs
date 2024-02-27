public class Post
{
    public int Id { get; set; }
    public string Text {get; set;}
    public List<Tag> Tags { get; } = new List<Tag>();
}