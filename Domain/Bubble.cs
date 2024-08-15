namespace Domain
{
    public class Bubble
    {
        public string Message { get; set; } = null!;
        public DateTime RemoveAt { get; set; } = DateTime.Now.AddSeconds(20);

        public string InitialX { get; set; }
        public string InitialY { get; set; }
        public string FinalX { get; set; }
        public string FinalY { get; set; }
    }
}
