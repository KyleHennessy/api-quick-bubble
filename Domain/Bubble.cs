namespace Domain
{
    public class Bubble
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; } = null!;
        public string Colour { get; set; } = null!;
    }
}
