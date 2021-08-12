using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200,ErrorMessage = ValidationMessages.MaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200, ErrorMessage = ValidationMessages.MaxLength)]
        [EmailAddress(ErrorMessage = ValidationMessages.EmailType)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)]
        public string Message { get; set; }

        public long ProductId { get; set; }
    }
}