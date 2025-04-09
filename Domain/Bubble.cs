using Application.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Bubble
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [MaxLength(250, ErrorMessage = "Messages can be no more than 250 characters long")]
        [Required]
        [Profanity]
        public required string Message
        {
            get => _message;
            set
            {
                var filter = new ProfanityFilter.ProfanityFilter();
                filter.RemoveProfanity("hell");
                filter.RemoveProfanity("bloody hell");

                var words = value.Split(" ");

                for(int i = 0; i < words.Length; i++)
                {
                    if (filter.ContainsProfanity(words[i]) && !words[i].Contains("ship"))
                    {
                        var sb = new StringBuilder(words[i]);
                        for (int j = 0; j < sb.Length; j++)
                        {
                            sb[j] = '*';
                        }
                        words[i] = sb.ToString();
                    }
                }

                _message = string.Join(" ", words);
            }
        }

        private string _message = string.Empty;

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "This is not a valid hexadecimal colour code")]
        [Required]
        public required string Colour { get; set; } = null!;

        [FileValidation]
        public string? Background { get; set; }
    }
}
