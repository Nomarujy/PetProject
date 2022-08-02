namespace Portfolio.Models.Authorization
{
    public class Group
    {
        public string Name { get; set; } = string.Empty;
        public Role[]? Roles { get; set; } = null!;
    }
}
