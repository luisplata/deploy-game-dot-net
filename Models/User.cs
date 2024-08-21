public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public int MaxTry { get; set; } = 3;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Key> Keys { get; set; } = new List<Key>();
}
