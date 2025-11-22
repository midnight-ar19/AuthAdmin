using System.ComponentModel.DataAnnotations;

namespace AuthAdmin.DTOs
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string NombreApellido { get; set; }
        public string Correo { get; set; }
    }
}
