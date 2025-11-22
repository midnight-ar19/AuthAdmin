using System.ComponentModel.DataAnnotations;

namespace AuthAdmin.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Correo {  get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
