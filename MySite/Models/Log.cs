using System.ComponentModel.DataAnnotations;

namespace MySite.Models
{
    public class Log
    {
        [Required(ErrorMessage ="Обязательно к заполнению")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string? Password { get; set; }
    }
}
