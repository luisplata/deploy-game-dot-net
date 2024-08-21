public class Key
{
    public string KeyValue { get; set; } = string.Empty;
    public bool IsUsed { get; set; }
    public int? UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User User { get; set; } = new();
}
