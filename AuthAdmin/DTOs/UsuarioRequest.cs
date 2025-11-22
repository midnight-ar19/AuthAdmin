using System.ComponentModel.DataAnnotations;

namespace AuthAdmin.DTOs
{
    public class UsuarioRequest
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
