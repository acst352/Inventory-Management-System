using System.ComponentModel.DataAnnotations;

namespace RPInventarios.Models;

public class Usuarios
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "El apellido del usuario es obligatorio.")]
    public string Apellido { get; set; } = string.Empty;
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [DataType(DataType.EmailAddress)]
    public string Correo { get; set; } = string.Empty;
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [DataType(DataType.PhoneNumber)]
    [MinLength(10, ErrorMessage = "El teléfono debe contener al menos 10 caracteres.")]
    public string Telefono { get; set; } = string.Empty;
}
