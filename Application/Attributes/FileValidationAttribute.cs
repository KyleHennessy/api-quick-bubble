using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Attributes
{
    public class FileValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str))
            {
                return ValidationResult.Success;
            }

            var fileBytes = Convert.FromBase64String(str);

            var fileSizeInMB = fileBytes.Length / (1024.0 * 1024.0);

            //Max file size is 5MB
            if(fileSizeInMB > 5)
            {
                return new ValidationResult("Image is too large");
            }

            if (!IsImage(fileBytes))
            {
                return new ValidationResult("File is not an image");
            }

            return ValidationResult.Success;
        }

        static bool IsImage(byte[] bytes)
        {
            try
            {
                using (var image = SKBitmap.Decode(bytes))
                {
                    return image != null;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
