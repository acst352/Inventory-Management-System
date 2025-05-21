using System.ComponentModel.DataAnnotations;

namespace RPInventarios.Models;

public class Marca
{
    public int Id { get; set; }
    [Required(ErrorMessage = "La marca es requerida")]
    [MinLength(5, ErrorMessage = "La marca debe tener al menos 5 caracteres")]
    [MaxLength(50, ErrorMessage = "La marca no puede tener más de 50 caracteres")]
    [Display(Name = "Marca")]
    public string Nombre { get; set; }
    public virtual ICollection<Producto> Productos { get; set; }
    // Se usa ICollection<Producto> para representar la relación 1:N entre
    // Marca y Producto.
}