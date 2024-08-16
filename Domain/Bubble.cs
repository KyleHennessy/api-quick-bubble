namespace Domain
{
    public class Bubble
    {
        private static readonly Random rand = new Random();

        public string Message { get; set; } = null!;
        public string Colour { get; set; } = null!;
        public DateTime RemoveAt { get; set; } = DateTime.Now.AddSeconds(20);
        public int InitialX { get; set; } = rand.Next(1, 101);
        public int InitialY { get; set; } = rand.Next(1, 101);
        public int FinalX { get; set; } = rand.Next(1, 101);
        public int FinalY { get; set; } = rand.Next(1, 101);
    }
}
