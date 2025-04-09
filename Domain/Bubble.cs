using Application.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Bubble
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(250, ErrorMessage = "Messages can be no more than 250 characters long")]
        [Profanity]
        public required string Message
        {
            get => _message;
            set
            {
                var filter = new ProfanityFilter.ProfanityFilter();
                filter.RemoveProfanity("hell");
                filter.RemoveProfanity("bloody hell");

                _message = filter.CensorString(value);
                if (filter.ContainsProfanity(_message))
                {
                    var sb = new StringBuilder(_message);
                    for(int i = 0; i < sb.Length; i++)
                    {
                        if (sb[i] != ' ')
                        {
                            sb[i] = '*';
                        }
                    }
                    _message = sb.ToString();
                }
            }
        }

        private string _message = string.Empty;

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "This is not a valid hexadecimal colour code")]
        public required string Colour { get; set; } = null!;

        [FileValidation]
        public string? Background { get; set; }
    }
}
