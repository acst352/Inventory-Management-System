using System.ComponentModel.DataAnnotations;

namespace RPInventarios.Models;
public class Departamento
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del departamento es requerido")]
    [MinLength(5, ErrorMessage = "El nombre del departamento debe tener al menos 5 caracteres")]
    [MaxLength(50, ErrorMessage = "El nombre del departamento no puede tener más de 50 caracteres")]
    [Display(Name = "Departamento")]
    public string Nombre { get; set; }
}
