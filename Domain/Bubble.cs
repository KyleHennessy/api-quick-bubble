namespace Domain
{
    public class Bubble
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Message { get; set; } = null!;
        public required string Colour { get; set; } = null!;
        public string? Background { get; set; }
    }
}
