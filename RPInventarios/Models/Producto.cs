using System.ComponentModel.DataAnnotations;
namespace RPInventarios.Models;

public enum EstatusProducto
{
    Inactivo = 0,
    Activo = 1,
    [Display(Name = "En proceso de Activación")]
    EnProcesoActivacion = 2
}

public class Producto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del producto es requerido")]
    [MinLength(5, ErrorMessage = "El nombre del producto debe tener al menos 5 caracteres")]
    [MaxLength(50, ErrorMessage = "El nombre del producto no puede tener más de 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    [Display(Name = "Descripcion")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripcion del producto debe tener minimo 5 caracteres y /*máximo*/ 200")]
    public string Descripcion { get; set; } = string.Empty;
    [Display(Name = "Marca")]
    [Required(ErrorMessage = "La marca es requerida")]
    public int MarcaId { get; set; }
    public virtual Marca? Marca { get; set; }
    [Required(ErrorMessage = "El costo es requerido")]
    [Range(0, 9999999999, ErrorMessage = "El costo no puede tener más de 10 dígitos")]
    public decimal Costo { get; set; }
    public EstatusProducto Estatus { get; set; } = EstatusProducto.Activo;
}
