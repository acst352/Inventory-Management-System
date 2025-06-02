using System.ComponentModel.DataAnnotations;

namespace RPInventarios.Models;

public class Usuarios
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del usuario es obligatorio.")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "El apellido del usuario es obligatorio.")]
    public string Apellido { get; set; }
    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
    [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo electrónico debe ser válido y no contener caracteres especiales.")]
    [Display(Name = "Correo Electrónico")]
    [DataType(DataType.EmailAddress)]
    public string Correo { get; set; }
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El formato del teléfono es inválido.")]
    [StringLength(15, ErrorMessage = "El teléfono no puede exceder los 15 caracteres.")]
    [RegularExpression(@"^\+?[0-9\s\-()]+$", ErrorMessage = "El teléfono debe ser un número válido, permitiendo espacios, guiones y paréntesis.")]
    [Display(Name = "Teléfono")]
    [DataType(DataType.PhoneNumber)]
    [MinLength(10, ErrorMessage = "El teléfono debe contener al menos 10 caracteres.")]
    public string Telefono { get; set; }
}
