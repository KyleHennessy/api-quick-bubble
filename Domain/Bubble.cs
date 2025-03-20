using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Bubble
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(250, ErrorMessage = "Messages can be no more than 250 characters long")]
        public required string Message { get; set; } = null!;

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "This is not a valid hexadecimal colour code")]
        public required string Colour { get; set; } = null!;

        public string? Background { get; set; }
    }
}
