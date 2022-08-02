namespace Portfolio.Models.Authorization
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Role[]? Roles { get; set; } = null!;
    }
}
