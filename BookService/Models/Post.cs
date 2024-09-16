public class Post
{
    public int Id { get; set; }
    public string Title {get; set;}
    public List<Tag> Tags { get; }
}